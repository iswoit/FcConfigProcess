using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FcConfigProcess
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {

            try
            {
                /* 处理逻辑
                 * 0 输入校验
                 * 1&2 获取yyb最大值，+1就是新值
                 * 3&4 填充所有相关段
                 */


                // 0.0 输入参数准备
                string filePath = tbFilePath.Text.Trim();               // 文件路径
                string clientID = tbClientID.Text.Trim();               // 客户号
                string stockHolder = tbStockHolder.Text.Trim();         // 股东号别名
                string yyb = tbYYB.Text.Trim();                         // 营业部
                string organization = tbOrganization.Text.Trim();       // 机构简称
                string productName = tbProductName.Text.Trim();         // 产品名称
                string stockHolderRef = tbStockHolderReference.Text.Trim();     // 参看股东号

                int newMaxNum = 0;          // 新配置序号
                bool isStockHolderRefFound = false;
                string[] keys, values;      // ini文件中键值对临时变量


                // 0.1 输入规则校验
                // 文件路径
                if (filePath.Length == 0)
                {
                    MessageBox.Show("请选择配置文件!");
                    btnSelFile.Focus();
                    return;
                }

                // 客户号
                if (!Regex.IsMatch(clientID, @"^\d{12}$"))
                {
                    MessageBox.Show("请输入12位纯数字的客户号!");
                    tbClientID.Focus();
                    return;
                }

                // 股东号别名
                if (!Regex.IsMatch(stockHolder, @"^[a-z]{3}_[0-9]{2}$"))
                {
                    MessageBox.Show("确保股东号别名格式的正确性(3位小写字母_2位数字)!");
                    tbStockHolder.Focus();
                    return;
                }

                // 机构简称
                if (organization.Length == 0)
                {
                    MessageBox.Show("请输入机构简称!");
                    tbOrganization.Focus();
                    return;
                }

                // 产品名
                if (productName.Length == 0)
                {
                    MessageBox.Show("请输入产品名称!");
                    tbProductName.Focus();
                    return;
                }

                // 参考股东号别名
                if (!Regex.IsMatch(stockHolderRef, @"^[a-z]{3}_[0-9]{2}$"))
                {
                    MessageBox.Show("确保【参考股东号别名】格式的正确性(3位小写字母_2位数字)!");
                    tbStockHolderReference.Focus();
                    return;
                }




                // 1.判断[gdzhlb]段中客户号&股东号别名的重复性；计算最大的营业部值
                INIHelper.GetAllKeyValues("gdzhlb", out keys, out values, filePath);
                for (int i = 0; i < keys.Length; i++)
                {

                    // gdzhxxx判断股东号别名重复；以及参考股东号存在性判断
                    if (Regex.IsMatch(keys[i], @"^gdzh\d{1,}$"))
                    {
                        string[] elements = values[i].Split(new char[] { ',' });
                        if (string.Equals(elements[0].Trim(), stockHolder))
                        {
                            MessageBox.Show(string.Format(@"股东账号别名：{0} 已经存在（[gdzhlb]配置项{1}）！无法重复添加！", stockHolder, keys[i]));
                            tbStockHolder.Focus();
                            return;
                        }

                        int tmpNum = int.Parse(keys[i].Substring(4).Trim());
                        if (tmpNum > newMaxNum)
                            newMaxNum = tmpNum;


                        // 参考股东号存在性判断
                        if (isStockHolderRefFound == false && string.Equals(elements[0].Trim(), stockHolderRef))
                            isStockHolderRefFound = true;
                    }

                    // sqlxxx判断客户号重复
                    if (Regex.IsMatch(keys[i], @"^sql\d{1,}$"))
                    {
                        if (values[i].Contains(clientID))    // 如果客户号存在，提示报错
                        {
                            MessageBox.Show(string.Format(@"客户号：{0} 已经存在（[gdzhlb]配置项{1}）！无法重复添加！", clientID, keys[i]));
                            tbClientID.Focus();
                            return;
                        }

                        int tmpNum = int.Parse(keys[i].Substring(3).Trim());
                        if (tmpNum > newMaxNum)
                            newMaxNum = tmpNum;
                    }
                }


                // 2.参考股东号
                if (isStockHolderRefFound == false)
                {
                    MessageBox.Show(string.Format(@"参考股东号别名：{0} 不存在! 无法复制！", stockHolderRef));
                    tbStockHolderReference.Focus();
                    return;
                }

                // 2.判断[yyb]段中的重复段
                INIHelper.GetAllKeyValues("yyb", out keys, out values, filePath);
                for (int i = 0; i < keys.Length; i++)
                {
                    if (Regex.IsMatch(keys[i], @"^yyb\d{1,}$"))
                    {
                        string[] elements = values[i].Split(new char[] { ',' });
                        if (string.Equals(elements[0], yyb))
                        {
                            MessageBox.Show(string.Format(@"营业部：{0} 已经存在（[yyb]配置项{1}）！无法重复添加！", yyb, keys[i]));
                            return;
                        }
                    }

                    int tmpNum = int.Parse(keys[i].Substring(3).Trim());
                    if (tmpNum > newMaxNum)
                        newMaxNum = tmpNum;
                }




                newMaxNum++;    // 搜索出来的最大值+1就是新的id



                // 3.插入[gdzhlb]、[yyb]
                string tmpKey, tmpValue;

                // [gdzhlb]
                tmpKey = string.Format(@"gdzh{0}", newMaxNum);
                tmpValue = string.Format(@"{0},sql,sql{1},jzpt", stockHolder, newMaxNum.ToString());
                INIHelper.Write("gdzhlb", tmpKey, tmpValue, filePath);

                tmpKey = string.Format(@"sql{0}", newMaxNum);
                tmpValue = string.Format(@"select client_id  khh,stock_account  gdh from dsg.vingdh where client_id='{0}'", clientID);
                INIHelper.Write("gdzhlb", tmpKey, tmpValue, filePath);

                // [yyb]
                tmpKey = string.Format(@"yyb{0}", newMaxNum);
                tmpValue = string.Format(@"{0},{1}－{2}", yyb, organization, productName);
                INIHelper.Write("yyb", tmpKey, tmpValue, filePath);


                // 4.[fjfile]段根据推荐项进行复制
                INIHelper.GetAllKeyValues("fjfile", out keys, out values, filePath);



                //INIHelper.EraseSection("fjfile", filePath);
                //INIHelper.Write("fjfile", "sss", "dsad", filePath);

                MessageBox.Show("处理完成!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSelFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "配置文件(*.ini)|*.ini|所有文件(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    tbFilePath.Text = dlg.FileName;
                }
            }
        }
    }
}
