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


                // 0.变量
                string filePath = tbFilePath.Text.Trim();               // 文件路径
                string clientID = tbClientID.Text.Trim();               // 客户号
                string stockHolder = tbStockHolder.Text.Trim();         // 股东号别名
                string yyb = tbYYB.Text.Trim();                         // 营业部
                string organization = tbOrganization.Text.Trim();       // 机构简称
                string productName = tbProductName.Text.Trim();         // 产品名称
                string stockHolderRef = tbStockHolderReference.Text.Trim();     // 参考股东号
                int stockHolderRefNum = -1;                                     // 参考股东号的序号，用来查参考营业部号的
                string yybRef = string.Empty;                                   // 参考营业部

                int newMaxNum = 0;          // 新配置序号
                bool isStockHolderRefFound = false;
                string[] keys, values;      // ini文件中键值对临时变量


                // 1.输入参数合格性校验
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




                // 2.准备yyb和gdzh：判断[gdzhlb]段中客户号&股东号别名的重复性；计算最大的营业部值
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
                        {
                            isStockHolderRefFound = true;
                            stockHolderRefNum = int.Parse(keys[i].Substring(4));    // 股东号序号，用来找营业部的
                        }
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




                // 参考股东号数据生成
                if (isStockHolderRefFound == false)
                {
                    MessageBox.Show(string.Format(@"参考股东号别名：{0} 不存在! 无法复制！", stockHolderRef));
                    tbStockHolderReference.Focus();
                    return;
                }

                // 判断[yyb]段中的重复段
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

                        // 找参看营业部号
                        if (int.Parse(keys[i].Substring(3).Trim()) == stockHolderRefNum)
                        {
                            yybRef = elements[0].Trim();
                        }
                    }

                    int tmpNum = int.Parse(keys[i].Substring(3).Trim());
                    if (tmpNum > newMaxNum)
                        newMaxNum = tmpNum;
                }


                if(string.IsNullOrEmpty(yybRef))
                {
                    MessageBox.Show(string.Format(@"没有找到参考股东号{0}对应的营业部！请检查配置文件！", stockHolderRef));
                    tbStockHolderReference.Focus();
                    return;
                }


                newMaxNum++;    // 搜索出来的最大值+1就是新的id




                // 3.[fjfile]段根据推荐项进行复制
                INIHelper.GetAllKeyValues("fjfile", out keys, out values, filePath);

                // 整理好的字典，后期替换原配置文件的[fjfile]段
                Dictionary<string, string> dicNew = new Dictionary<string, string>();
                int sourceNum = -1;
                int sourceNumMax = -1;
                bool isRefFound = false;
                string refValue = string.Empty;

                for (int i = 0; i < keys.Length; i++)
                {
                    /* 1.如果key是sourX，解析出X值，更新到变量；判断上一个sourX是否找到参考项，有的话解析出后插入dicNew
                     * 2.如果key是DestXY，解析出X和Y（Y是3位数字）：如果X和当前变量不一致，报错。同时记录Y的最大值(要验证递增)。
                     */

                    if (Regex.IsMatch(keys[i], @"^sour\d{1,}$")) // 匹配到sourX
                    {
                        // 处理上一个sour的
                        if (isRefFound == true)    // 如果上一个sour找到参照dest，则插入dicNew
                        {
                            dicNew.Add(string.Format(@"Dest{0}{1}", sourceNum.ToString(), (sourceNumMax + 1).ToString().PadLeft(3, '0')), refValue);
                        }


                        // 开始新的sour
                        string tmpStr = keys[i].Substring(4).Trim();
                        if (!int.TryParse(tmpStr, out sourceNum))
                            throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(sour+数字)!操作中断!", keys[i]));

                        sourceNumMax = 0;
                        isRefFound = false;
                        refValue = string.Empty;

                        dicNew.Add(keys[i], values[i]);
                    }
                    else if (Regex.IsMatch(keys[i], @"^Dest\d{1,}$"))    // 匹配到DestXYYY
                    {

                        // 判断格式长度
                        string tmpStr = keys[i].Substring(4).Trim();    // XYYY数字串
                        if (tmpStr.Length != sourceNum.ToString().Length + 3)
                            throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(Dest+sour数字+3位序号)!操作中断!", keys[i]));

                        // 判断X位是否匹配
                        int tmpX = -999;
                        int tmpXLength = tmpStr.Length - 3; // 长度-3
                        if (!int.TryParse(tmpStr.Substring(0, tmpStr.Length - 3), out tmpX))
                            throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(Dest+sour数字+3位序号)!操作中断!", keys[i]));
                        if (tmpX != sourceNum)
                            throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(DestXYYY没有紧跟sourX)!操作中断!", keys[i]));

                        // 解析YYY，并且需要比之前的大1
                        int tmpYYY = 0;
                        if (!int.TryParse(tmpStr.Substring(tmpStr.Length - 3), out tmpYYY))
                            throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(DestXYYY，YYY必须为数字)!操作中断!", keys[i]));
                        if (tmpYYY == sourceNumMax + 1)
                            sourceNumMax++;
                        else
                            throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(DestXYYY，YYY必须为上一行的值加1，不连续)!操作中断!", keys[i]));

                        // 查找参考项
                        if (values[i].Contains(stockHolderRef))
                        {
                            if (isRefFound == true)
                                throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(参考项判断，在sour下有重复)!操作中断!", keys[i]));
                            else if (isRefFound == false)  // 找到参考股东号，重新定义格式(格式为4段，第一段截取\后的)
                            {
                                isRefFound = true;

                                // 处理新的参数
                                string[] tmpArr = values[i].Split(',');
                                if (tmpArr.Length != 4)
                                    throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(Dest应该为4部分)!操作中断!", keys[i]));

                                if (!tmpArr[0].Contains(@"\"))
                                    throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(Dest第一部分要有\分隔符)!操作中断!", keys[i]));

                                refValue = string.Format(@"{0}{1},{2},{3},{4}",
                                    yyb,
                                    tmpArr[0].Substring(tmpArr[0].LastIndexOf('\\')),
                                    tmpArr[1],
                                    tmpArr[2],
                                    stockHolder);
                            }
                        }

                        dicNew.Add(keys[i], values[i]);
                    }
                }
                //尾数处理
                if (isRefFound == true)    // 如果上一个sour找到参照dest，则插入dicNew
                {
                    dicNew.Add(string.Format(@"Dest{0}{1}", sourceNum.ToString(), (sourceNumMax + 1).ToString().PadLeft(3, '0')), refValue);
                }


                // 最后的路径
                if (!INIHelper.ExistSection(yybRef, filePath))
                    throw new Exception(string.Format(@"[{0}]节不存在，请检查!操作中断!", yybRef));
                //
                // 最终复制的路径段
                Dictionary<string, string> dicNew_Path = new Dictionary<string, string>();
                INIHelper.GetAllKeyValues(yybRef, out keys, out values, filePath);
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i] == "path")
                        dicNew_Path.Add("path", tbDestPath.Text.Trim());
                    else if (keys[i].StartsWith("file"))
                    {
                        string[] tmpArr = values[i].Split('\\');
                        dicNew_Path.Add(keys[i], string.Format(@"{0}\{1}", yyb, tmpArr[1]));
                    }
                    else
                    {
                        dicNew_Path.Add(keys[i], values[i]);
                    }
                }



                // 4.插入新的节[gdzhlb]、[yyb]、[fjfile]、以及最后的路径
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

                // [最终路径段]
                foreach (KeyValuePair<string, string> tmpKV in dicNew_Path)
                {
                    INIHelper.Write(yyb, tmpKV.Key, tmpKV.Value, filePath);
                }

                // [fjfile]
                INIHelper.EraseSection("fjfile", filePath);     // 先清空
                INIHelper.Write("fjfile", @"&& 分解库文件段配置", string.Empty, filePath);
                INIHelper.Write("fjfile", @"&&sour?", @"库文件名,类型别名", filePath);
                INIHelper.Write("fjfile", @"&&dest???", @"目的文件(空表示不分解),分支席位号-...,起始合同号-结束合同号|...,股东帐号别名", filePath);
                foreach (KeyValuePair<string, string> tmpKV in dicNew)
                {
                    INIHelper.Write("fjfile", tmpKV.Key, tmpKV.Value, filePath);
                }




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

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
