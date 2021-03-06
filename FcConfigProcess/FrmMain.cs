﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace FcConfigProcess
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 配置文件选择-新增产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 新增产品逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecute_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("确认添加产品(一旦提示异常请恢复备份文件)？", "确认", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
                return;

            string filePath = string.Empty;         // 配置文件
            string filePathBak = string.Empty;      // 备份的配置文件
            try
            {
                /* 处理逻辑
                 * 0 输入校验
                 * 1&2 获取yyb最大值，+1就是新值
                 * 3&4 填充所有相关段
                 */


                // 0.变量
                filePath = tbFilePath.Text.Trim();               // 文件路径
                filePathBak = filePath + ".bak";                // 20180910 备份路径

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

                // 20180910 - 先备份一个新的配置文件，如果有异常可以恢复
                System.IO.File.Copy(filePath, filePathBak, true);

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


                // 20180910 - 如果选深证通，要填路径
                if (rbNormal.Checked)
                {
                    if (txtSZT.Text.Trim().Length <= 0)
                    {
                        MessageBox.Show("确保深证通文件夹名称已输入!");
                        txtSZT.Focus();
                        return;
                    }
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


                if (string.IsNullOrEmpty(yybRef))
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






                // 20180910 - 增加FileCopy生成

                string strCompanyShort = string.Empty;
                if (tbYYB.Text.Trim().Length == 5)
                    strCompanyShort = tbYYB.Text.Trim().Substring(0, 3);
                else if (tbYYB.Text.Trim().Length == 6)
                    strCompanyShort = tbYYB.Text.Trim().Substring(0, 4);

                string strFileCopy = string.Empty;
                strFileCopy += string.Format(@"fileXXXX=d:\BJS_Files\bjsgb.dbf,E:\FtpRoot\清算文件目录\{0}\{1}\bjsgb.dbf,3", strCompanyShort, productName) + System.Environment.NewLine;
                strFileCopy += string.Format(@"fileXXXX=e:\vsat\nqhq\nqxx.dbf,E:\FtpRoot\清算文件目录\{0}\{1}\nqxx.dbf,3", strCompanyShort, productName) + System.Environment.NewLine;
                strFileCopy += string.Format(@"fileXXXX=e:\vsat\nqhq\nqhq.dbf,E:\FtpRoot\清算文件目录\{0}\{1}\nqhq.dbf,3", strCompanyShort, productName) + System.Environment.NewLine;
                strFileCopy += string.Format(@"fileXXXX=E:\HFXN\QSK\xyfile\abcsjjs326.#mdd,E:\FtpRoot\清算文件目录\{0}\{1}\abcsj.dbf,3", strCompanyShort, productName) + System.Environment.NewLine;
                strFileCopy += string.Format(@"fileXXXX=E:\HFXN\QSK\xyfile\abcsjjsx78.#mdd,E:\FtpRoot\清算文件目录\{0}\{1}\abcsj_2r.dbf,3", strCompanyShort, productName) + System.Environment.NewLine;
                if (rbNormal.Checked)
                    strFileCopy += string.Format(@"fileXXXX=E:\FtpRoot\清算文件目录\{0}\{1}#yyyy#mm#dd.rar,N:\qsfile\FC\{2}\{1}\{1}#yyyy#mm#dd.rar,3", strCompanyShort, productName, txtSZT.Text.Trim());
                else if (rbJA.Checked)
                    strFileCopy += string.Format(@"fileXXXX=E:\FtpRoot\清算文件目录\{0}\{1}#yyyy#mm#dd.rar,N:\QsFile\OFSI\YWLX\JA\{1}#yyyy#mm#dd.rar,3", strCompanyShort, productName);
                else if (rbXYYH.Checked)
                    strFileCopy += string.Format(@"fileXXXX=E:\FtpRoot\清算文件目录\{0}\{1}#yyyy#mm#dd.rar,S:\TA兴业银行\SEND\{1}\{1}#yyyy#mm#dd.rar,3", strCompanyShort, productName);

                txtFileCopy.Text = strFileCopy;


                // 20180910 - 增加分仓bat生成
                string strFcBat = string.Empty;
                strFcBat += string.Format(@"call :CheckFileDateIsToday  {0}_{1}      %FTPDir%\{2}\{1}      FL_lhjj       %FTPDir%\{2}\{1}%rq8%.rar",
                    organization,
                    productName,
                    strCompanyShort);
                strFcBat += System.Environment.NewLine;
                strFcBat += System.Environment.NewLine;

                if (rbNormal.Checked)
                {
                    strFcBat += string.Format(@"call :CreateOKFile  {0}_{1}       	   %FDEPDir%\{2}\{1}\{1}%rq8%.rar",
                        organization,
                        productName,
                        txtSZT.Text.Trim());
                }
                else if (rbJA.Checked)
                {
                    strFcBat += string.Format(@"call :CreateOKFile  {0}_{1}       	           %FDEPJJDir%\JA\{1}%rq8%.rar",
                        organization,
                        productName);
                }
                else if (rbXYYH.Checked)
                {
                    strFcBat += string.Format(@"call :CreateOKFile  {0}_{1}       	   %FDEPDir2%\{1}\{1}%rq8%.rar",
                        organization,
                        productName);
                }

                txtFcBAT.Text = strFcBat;



                MessageBox.Show("处理完成!");



            }
            catch (Exception ex)
            {
                // 20180910 - 先备份一个新的配置文件，如果有异常可以恢复
                System.IO.File.Copy(filePathBak, filePathBak, true);
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 配置文件选择-删除产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelDelFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "配置文件(*.ini)|*.ini|所有文件(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    tbDelFilePath.Text = dlg.FileName;
                }
            }
        }

        /// <summary>
        /// 删除产品逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelExecute_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认删除产品(一旦提示异常请恢复备份文件)？", "确认", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
                return;



            /* 1.通过股东账号别名，搜索[gdzhlb]的条目和数字.删除
             * 2.删除[yyb]的条目和数字
             * 3.删除[营业部]节
             * 4.删除[fjfile]中关于此股东账号别名的行
             * 
             * 以上删除的，需要重排所有序号
             * 
             */


            try
            {
                // 0.输入参数准备
                string filePath = tbDelFilePath.Text.Trim();                // 配置文件路径（删除用）
                string stockHolder = tbDelStockHolder.Text.Trim();          // 股东号别名(删除用，要按回车处理)
                List<string> listStockHolder = new List<string>();          // 股东号别名列表
                Dictionary<string, string> dicStkHolderYYB = new Dictionary<string, string>();      // dic
                string[] keys, values;                                      // 用来查ini的临时变量

                // 文件路径
                if (filePath.Length == 0)
                {
                    MessageBox.Show("请选择配置文件!");
                    btnSelDelFile.Focus();
                    return;
                }

                // 股东号别名
                if (string.IsNullOrEmpty(stockHolder))
                {
                    MessageBox.Show("请至少输入一个股东号别名!");
                    tbDelStockHolder.Focus();
                    return;
                }

                string[] arrTmp = stockHolder.Split(Environment.NewLine.ToCharArray());
                foreach (string tmp in arrTmp)
                {
                    if (!string.IsNullOrEmpty(tmp.Trim()))
                        listStockHolder.Add(tmp.Trim());
                }


                #region 1.删除数据
                // 1.处理[ddzhlb]：（要找id，对应[yyb]的数据要删，对应[yyb]的yyb代码要找出来删）
                INIHelper.GetAllKeyValues("gdzhlb", out keys, out values, filePath);
                for (int i = 0; i < keys.Length; i++)       // 遍历[gdzhlb]节，找是否在删除名单内
                {
                    if (Regex.IsMatch(keys[i], @"^gdzh\d{1,}$"))
                    {
                        string[] arrGDZHLB = values[i].Split(',');  // 第0个是股东号别名
                        if (listStockHolder.Contains(arrGDZHLB[0].Trim()))   // 如果是在删除名单内，处理
                        {
                            // 获取yyb（为了删除yyb和[营业部]节）
                            string yybKey = string.Format(@"yyb{0}", keys[i].Substring(4)); //营业部的key键

                            if (INIHelper.ExistKey("yyb", yybKey, filePath))
                            {
                                string yybValue = INIHelper.Read("yyb", yybKey, filePath);
                                string yyb = yybValue.Split(',')[0].Trim();    // 营业部


                                //********重要逻辑，开始删了
                                INIHelper.DeleteKey("gdzhlb", keys[i], filePath);   // 删除[gdzhlb]的gdzh
                                INIHelper.DeleteKey("gdzhlb", string.Format(@"sql{0}", keys[i].Substring(4)), filePath);    // 删除[gdzhlb]的sql

                                INIHelper.DeleteKey("yyb", yybKey, filePath);       // 2.删除[yyb]的条目和数字

                                INIHelper.EraseSection(yyb, filePath);              // 3.删除[营业部]节


                                // 临时，校验用
                                dicStkHolderYYB.Add(arrGDZHLB[0].Trim(), yyb);
                            }
                            else
                            {
                                throw new Exception(string.Format(@"营业部{0}不存在，请检查配置文件！", yybKey));
                            }


                            // 删除[营业部]节

                            // 遍历[fjfile]节，删除所有DestXXX包含营业部别名的键值
                        }

                    }
                }


                // 4.删除[fjfile]中关于此股东账号别名的行
                INIHelper.GetAllKeyValues("fjfile", out keys, out values, filePath);
                for (int i = 0; i < keys.Length; i++)       // 遍历[fjfile]节，找是否在删除名单内
                {
                    if (Regex.IsMatch(keys[i], @"^Dest\d{1,}$"))
                    {
                        string[] arrFJFILE = values[i].Split(',');  // 第4个是股东号别名
                        string tmpStockHolder = arrFJFILE[3].Trim();



                        if (listStockHolder.Contains(tmpStockHolder))    // 删除
                        {
                            // 多一个股东号别名+营业部的校验
                            if (dicStkHolderYYB[tmpStockHolder] != arrFJFILE[0].Split('\\')[0].Trim())
                                throw new Exception(string.Format("{0}配置项营业部和股东号别名对不上![yyb]为{0}，当前配置为{2}", keys[i], dicStkHolderYYB[tmpStockHolder], arrFJFILE[0].Split('\\')[0].Trim()));


                            INIHelper.DeleteKey("fjfile", keys[i], filePath);
                        }
                    }
                }
                #endregion 1.删除数据


                #region 2.重新排序

                // 1.[gdzhlb]重排
                Dictionary<string, string> dicNewGDZHLB = new Dictionary<string, string>();
                // 插入注释
                dicNewGDZHLB.Add(@"&&股东帐号别名配置段", string.Empty);
                dicNewGDZHLB.Add(@"&&gdzh?", @"别名(只取前6位),帐号来源(sql或dbf),sql语句别名或dbf库文件名,sql数据库连接别名或dbf帐号字段名(默认为gdh)");
                dicNewGDZHLB.Add(@"&&sql语句别名", @"sql语句(结果中必须包含GDH字段)");

                int iNewCnt = 1;    // 新序号
                INIHelper.GetAllKeyValues("gdzhlb", out keys, out values, filePath);
                for (int i = 0; i < keys.Length; i++)       // 遍历[gdzhlb]节，找是否在删除名单内
                {
                    if (Regex.IsMatch(keys[i], @"^gdzh\d{1,}$"))
                    {
                        string[] tmpArr = values[i].Split(',');
                        if (tmpArr.Length != 4)
                            throw new Exception(keys[i] + "值没有分4段，请检查！");

                        if (!INIHelper.ExistKey("gdzhlb", tmpArr[2].Trim(), filePath))
                            throw new Exception(tmpArr[2].Trim() + "不存在，请检查！");

                        string tmpSqlValue = INIHelper.Read("gdzhlb", tmpArr[2].Trim(), filePath);

                        // 插入dicNewGDZHLB字典
                        dicNewGDZHLB.Add(string.Format(@"gdzh{0}", iNewCnt), string.Format(@"{0},{1},sql{2},{3}", tmpArr[0], tmpArr[1], iNewCnt, tmpArr[3]));
                        dicNewGDZHLB.Add(string.Format(@"sql{0}", iNewCnt), tmpSqlValue);

                        iNewCnt++;
                    }
                }

                // 清除再插入
                INIHelper.EraseSection("gdzhlb", filePath);     // 先清空
                foreach (KeyValuePair<string, string> tmpKV in dicNewGDZHLB)
                {
                    INIHelper.Write("gdzhlb", tmpKV.Key, tmpKV.Value, filePath);
                }



                // 2.[yyb]重排
                iNewCnt = 1;
                Dictionary<string, string> dicNewYYB = new Dictionary<string, string>();
                INIHelper.GetAllKeyValues("yyb", out keys, out values, filePath);
                for (int i = 0; i < keys.Length; i++)       // 遍历[gdzhlb]节，找是否在删除名单内
                {
                    if (Regex.IsMatch(keys[i], @"^yyb\d{1,}$"))
                    {
                        dicNewYYB.Add(string.Format(@"yyb{0}", iNewCnt), values[i]);

                        iNewCnt++;
                    }
                }

                // 清除再插入
                INIHelper.EraseSection("yyb", filePath);     // 先清空
                foreach (KeyValuePair<string, string> tmpKV in dicNewYYB)
                {
                    INIHelper.Write("yyb", tmpKV.Key, tmpKV.Value, filePath);
                }



                // 3.[fjfile]重排
                iNewCnt = 1;
                Dictionary<string, string> dicNewFJFILE = new Dictionary<string, string>();
                dicNewFJFILE.Add(@"&&分解库文件段配置", string.Empty);
                dicNewFJFILE.Add(@"&&sour?", @"库文件名,类型别名");
                dicNewFJFILE.Add(@"&&dest???", @"目的文件(空表示不分解),分支席位号-...,起始合同号-结束合同号|...,股东帐号别名");

                int sourceNum = -1; // 当前的sour值
                INIHelper.GetAllKeyValues("fjfile", out keys, out values, filePath);
                for (int i = 0; i < keys.Length; i++)
                {
                    if (Regex.IsMatch(keys[i], @"^sour\d{1,}$")) // 匹配到sourX
                    {
                        // 开始新的sour
                        string tmpStr = keys[i].Substring(4).Trim();
                        if (!int.TryParse(tmpStr, out sourceNum))
                            throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(sour+数字)!操作中断!", keys[i]));

                        iNewCnt = 1;

                        dicNewFJFILE.Add(keys[i], values[i]);   // sour的保留
                    }
                    else if (Regex.IsMatch(keys[i], @"^Dest\d{1,}$"))    // 匹配到DestXYYY
                    {
                        if (sourceNum == -1)
                            throw new Exception("[fjfile]有问题，不是以sour开始!");

                        string tmpKey = string.Format(@"Dest{0}{1}", sourceNum, iNewCnt.ToString().PadLeft(3, '0'));
                        dicNewFJFILE.Add(tmpKey, values[i]);

                        iNewCnt++;
                    }
                }

                // 清除再插入
                INIHelper.EraseSection("fjfile", filePath);     // 先清空
                foreach (KeyValuePair<string, string> tmpKV in dicNewFJFILE)
                {
                    INIHelper.Write("fjfile", tmpKV.Key, tmpKV.Value, filePath);
                }


                #endregion 2.重新排序

                MessageBox.Show("删除产品处理完成!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnSelCheck_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "配置文件(*.ini)|*.ini|所有文件(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    tbCheckFilePath.Text = dlg.FileName;
                }
            }
        }

        /// <summary>
        /// 检查文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            // 文件路径
            string filePath = tbCheckFilePath.Text.Trim();
            if (filePath.Length == 0)
            {
                MessageBox.Show("请选择配置文件!");
                btnSelCheckFile.Focus();
                return;
            }

            /* 1.检查[gdzhlb]是否按顺序排列、sqlx是否有对应的存在
             * 2.检查[yyb]是否按照顺序排列、与1检查结果形成键值对
             * 3.检查[营业部]节是否存在
             * 4.检查[fjfile]节、sourX后跟DestXYYY，YYY按升序排列。同时满足2和1形成的键值对
             */

            try
            {
                Dictionary<string, string> dicCheck = new Dictionary<string, string>();

                string[] keys, values;                                      // 用来存放ini的临时变量

                // 1.检查[gdzhlb]、2.检查[yyb]
                if (!INIHelper.ExistSection("gdzhlb", filePath))
                {
                    MessageBox.Show("配置文件中没有[gdzhlb]!请选择正确的配置文件!");
                    return;
                }

                INIHelper.GetAllKeyValues("gdzhlb", out keys, out values, filePath);
                int iGDZHCnt = 0;   // gdzh的序列
                for (int i = 0; i < keys.Length; i++)       // 遍历[gdzhlb]节，找是否在删除名单内
                {
                    if (Regex.IsMatch(keys[i], @"^gdzh\d{1,}$"))
                    {
                        int iTmp;
                        if (!int.TryParse(keys[i].Substring(4).Trim(), out iTmp))
                            throw new Exception(string.Format(@"[gdzhlb]的键{0}命名不合法，应该为gdzh+数字", keys[i]));

                        if (iTmp != iGDZHCnt + 1)
                            throw new Exception(string.Format(@"[gdzhlb]的键{0}命名不合法，与上一个gdzh不连续", keys[i]));

                        string[] tmpArr = values[i].Split(',');
                        if (tmpArr.Length != 4)
                            throw new Exception(string.Format(@"[gdzhlb]的键{0}的值不合法，应该为4个", keys[i]));

                        string tmpKey = tmpArr[2].Trim();
                        if (!Regex.IsMatch(tmpKey, @"^sql\d{1,}$"))
                            throw new Exception(string.Format(@"[gdzhlb]的键{0}的值不合法，第3列应该为sql+数字", keys[i]));

                        if (!INIHelper.ExistKey("gdzhlb", tmpKey, filePath))
                            throw new Exception(string.Format(@"[gdzhlb]的键{0}缺少与之对应的键{1}", keys[i], tmpKey));


                        // 同时检查[yyb]，并形成键值对
                        if (!INIHelper.ExistKey("yyb", string.Format(@"yyb{0}", iTmp), filePath))
                            throw new Exception(string.Format(@"[gdzhlb]的键{0}缺少与之对应的[yyb]键yyb{1}", keys[i], iTmp));

                        string tmpYYB = INIHelper.Read("yyb", string.Format(@"yyb{0}", iTmp), filePath);
                        if (tmpYYB.Split(',').Length != 2)
                            throw new Exception(string.Format(@"[yyb]的键yyb{0}应该有2列", iTmp));

                        dicCheck.Add(tmpArr[0].Trim(), tmpYYB.Split(',')[0].Trim());    // 添加到键值对

                        iGDZHCnt++;
                    }
                }


                // 2.检查[营业部]
                foreach (KeyValuePair<string, string> kv in dicCheck)
                {
                    if (!INIHelper.ExistSection(kv.Value, filePath))
                        throw new Exception(string.Format(@"[{0}]不存在，请检查！", kv.Value));
                }


                // 3.检查[fjfile]
                INIHelper.GetAllKeyValues("fjfile", out keys, out values, filePath);

                int sourceNum = -1;
                int sourceNumMax = -1;
                for (int i = 0; i < keys.Length; i++)
                {
                    /* 1.如果key是sourX，解析出X值，更新到变量；判断上一个sourX是否找到参考项，有的话解析出后插入dicNew
                     * 2.如果key是DestXY，解析出X和Y（Y是3位数字）：如果X和当前变量不一致，报错。同时记录Y的最大值(要验证递增)。
                     */

                    if (Regex.IsMatch(keys[i], @"^sour\d{1,}$")) // 匹配到sourX
                    {

                        // 开始新的sour
                        string tmpStr = keys[i].Substring(4).Trim();
                        if (!int.TryParse(tmpStr, out sourceNum))
                            throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(sour+数字)!操作中断!", keys[i]));

                        sourceNumMax = 0;
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
                        string[] tmpArr = values[i].Split(',');
                        if (tmpArr.Length != 4)
                            throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(value必须是4列)!操作中断!", keys[i]));

                        if (dicCheck.ContainsKey(tmpArr[3].Trim()))
                        {
                            if (dicCheck[tmpArr[3].Trim()] != tmpArr[0].Split('\\')[0].Trim())
                                throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(营业部和股东代码别名不匹配)!操作中断!", keys[i]));
                        }
                        else
                        {
                            throw new Exception(string.Format(@"[fjfile]的键{0}不符合规则(股东号别名不存在)!操作中断!", keys[i]));
                        }

                    }
                }

                MessageBox.Show("检查通过!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void GeneratePathChanged(object sender, EventArgs e)
        {
            string strTmp = string.Empty;
            if (tbYYB.Text.Trim().Length == 5)
                strTmp = tbYYB.Text.Trim().Substring(0, 3);
            else if (tbYYB.Text.Trim().Length == 6)
                strTmp = tbYYB.Text.Trim().Substring(0, 4);

            tbDestPath.Text = string.Format(@"E:\FtpRoot\清算文件目录\{0}\{1}", strTmp, tbProductName.Text);
        }

        private void txt_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
                ((TextBox)sender).Cursor = System.Windows.Forms.Cursors.Arrow;  //指定鼠标形状（更好看）  
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void txt_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            ((TextBox)sender).Text = path;
            ((TextBox)sender).Cursor = System.Windows.Forms.Cursors.IBeam; //还原鼠标形状  
        }
    }
}
