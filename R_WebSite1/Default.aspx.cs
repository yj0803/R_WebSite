using System;
using System.Activities.Statements;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ionic.Zip;
using Microsoft.VisualBasic;
using StatConnControls;
using StatConnTools;
using STATCONNECTORCLNTLib;
using StatConnectorCommonLib;
using STATCONNECTORSRVLib;
using RDotNet;
using Microsoft.Win32;
using System.Management;
using Ionic.Zlib;
using System.Threading;

public class CreateREngine
{
   public REngine Ression()
    {
       REngine.SetEnvironmentVariables();
       return REngine.GetInstance();
    }
}

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinkButton1.Visible = false;
        LinkButton2.Visible = false;
        Image1.Visible = false;
        Image2.Visible = false;
        Page.MaintainScrollPositionOnPostBack = true;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DeleteFile();
        string R_path = Server.MapPath("./test.R");
        string RO_path = Server.MapPath("./test1.out");
        string str_cmd = "C: \n" +
                         "cd C:\\Program Files\\R\\R-3.2.1\\bin \n" +
                         "Rscript  --no-restore --no-save " + R_path + " " + RO_path + "  \n" +
                         "pause()";
        cmdShellFile(str_cmd,  Server.MapPath("./"), "cmd.bat","");
        Thread.Sleep(5000);
        /* 
        if (File.Exists(@"D:\Metatable\R_WebSite1\result\data.csv"))
        {
          
        }
        */
        if (File.Exists( Server.MapPath(@".\result\DataPic.png")))
        {
            Image1.Visible = true;
            Image1.ImageUrl = "./result/DataPic.png";
        }
        LinkButton1.Visible = true;
        //Process.Start(@"\\172.16.201.126\Metatable\R_WebSite1\cmd.bat");


        #region RDotNET

        /*
        CreateREngine CE = new CreateREngine();
        REngine engine = CE.Ression();
        if (engine.IsRunning == false)
        {
            engine.Initialize();
        }

        try
        {
            //讀取檔案
            engine.Evaluate("test <- read.csv(\"D:/Metatable/R_WebSite1/test.csv\")");
            //SCV檔 產生
            engine.Evaluate("aa <- as.data.frame(test)");
            engine.Evaluate("bb <-as.matrix(aa)");
            engine.Evaluate("write(t(bb), file = \"D:/Metatable/R_WebSite1/data.csv\",ncolumns=ncol(bb), sep = \",\")");
            LinkButton1.Enabled = true;           
            //圖形產生
            engine.Evaluate("require('ggplot2')");
            engine.Evaluate("library('ggplot2')");
            engine.Evaluate("jpeg(\"D:/Metatable/R_WebSite1/DataPic.jpg\")");
            engine.Evaluate("ggplot(aa,aes(x=TEMP,y=WSP))+geom_point()");
            engine.Evaluate("dev.off()");
            Image1.Visible = true;
            Label1.Text = "OK";           
            //TextBox1.Text = testResult.ToString();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            //LinkButton1.Enabled = false;
        }
        engine.Dispose();
        */

        #endregion
    }
    #region 檔案下載函式
    private void DownLoadFile(string parFilePath)
    {
        //將虛擬路徑轉換成實體路徑
        string FilePath = Server.MapPath(parFilePath);

        if (FilePath.Split('\\').Length != 0)
        {
            string FileName = FilePath.Split('\\')[FilePath.Split('\\').Length - 1];

            //中文檔名作轉換
            FileName = HttpUtility.UrlEncode(FileName, Encoding.UTF8);

            FileStream fr = new FileStream(FilePath, FileMode.Open);
            Byte[] buf = new Byte[fr.Length];

            fr.Read(buf, 0, Convert.ToInt32(fr.Length));
            fr.Close();
            fr.Dispose();

            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = true;
            //轉換文字檔編碼格式用，但本次輸出無文字檔，故註解此段
            //Response.ContentEncoding = parEncoding;
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName);

            Response.BinaryWrite(buf);
            Response.End();
        }
    }
    #endregion

    #region 暫時沒用
    /*
    private DataTable GetDataTable(string dataFrame)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds.Tables.Add(dataFrame);
        Array aryColumNames;
        Array aryValue;

        //建立連線
        StatConnector conn = new StatConnectorClass();

        //建立R的實例化
        conn.Init("R");

        //獲取data frame欄位名稱
        string cmdColumnNames = "colnames(" + dataFrame + ")";
        aryColumNames = (Array)conn.Evaluate(cmdColumnNames);

        //建立DataTable欄位名稱
        for (int i = 0; i < aryColumNames.Length; i++)
        {
            if (!ds.Tables[dataFrame].Columns.Contains(aryColumNames.GetValue(i).ToString())) ;
            {
                ds.Tables[dataFrame].Columns.Add(aryColumNames.GetValue(i).ToString().Trim().ToUpper());
            }
        }

        //獲取data frame的row數目
        string cmdNrow = "as.character(nrow(" + dataFrame + "))[1]";
        int nrow = int.Parse(Convert.ToString(conn.Evaluate(cmdNrow)));

        //產生空的row
        for (int i = 0; i < nrow; i++)
        {
            ds.Tables[dataFrame].Rows.Add();
        }

        //填滿每一個row
        for (int i = 0; i < aryColumNames.Length; i++)
        {
            string cmd = dataFrame + "$" + aryColumNames.GetValue(i).ToString();
            aryValue = null;
            aryValue = (Array)conn.Evaluate(cmd);
            for (int j = 0; j < aryValue.Length; j++)
            {
                ds.Tables[dataFrame].Rows[j][aryColumNames.GetValue(i).ToString()] = aryValue.GetValue(j).ToString();
            }
        }

        //關閉連線
        conn.Close();

        //傳回DataTable
        dt = ds.Tables[0];

        return dt;
    }*/
    #endregion

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ZipFiles( Server.MapPath(".\\result"), string.Empty, string.Empty);
        DownLoadFile("./result/result.zip");
    }

    #region cmdShellFile
    public static bool cmdShellFile(string cmd, string targetDir, string fileName,  string errMsg)
    {
        bool returnFlag = false;
        //return false as error,true as Success


        System.Diagnostics.Process process = new System.Diagnostics.Process();
        string cmdFullPath = targetDir + "\\" + fileName;

        try
        {
            FileSystem.FileOpen(1, cmdFullPath, OpenMode.Output);
            //PrintLine(1, test)
            FileSystem.PrintLine(1, "@echo off");
            //PrintLine(1, "shutdown.exe -r -f -t 1")
            FileSystem.PrintLine(1, cmd);
            //PrintLine(1, "sleep 5")
            //PrintLine(1, strGrantFtproot)
            //PrintLine(1, strSetFtpDir)

            //PrintLine(1, "pause")
            FileSystem.PrintLine(1, "EXIT");
            FileSystem.FileClose(1);
            Interaction.Shell(cmdFullPath, AppWinStyle.NormalFocus);
            long pid = 0;

            pid = Interaction.Shell("cmd.exe /c " + cmdFullPath, Constants.vbNormalFocus);

            returnFlag = true;

        }
        catch (Exception Ex)
        {
            errMsg = "Shell指令發生錯誤," + Ex.Message;
        }


        return returnFlag;
    }
    #endregion
    protected void Button2_Click(object sender, EventArgs e)
    {
        DeleteFile();
        string str_rcode = "test <- read.csv(\""+Server.MapPath("./test.csv ") + "\")\n "+
                           TextBox1.Text + "\n " +
                           TextBox_w1.Text + TextBox_w2.Text + ", file =\"" + Server.MapPath("./result/data.csv ")+"\"," + TextBox_w4.Text + TextBox_w5.Text + "\n" +
                            //"write(t(bb), file = \"" + Server.MapPath("./result/data.csv ")+"\",ncolumns=ncol(bb), sep = \",\") \n "+
                            TextBox5.Text+ "\n "+
                            "png(\"" + Server.MapPath("./result/DataPic.png ") + "\")"+ "\n "+
                            TextBox4.Text+"\n "+
                            TextBox7.Text;
        str_rcode = str_rcode.Replace(@"\", @"/");
        StreamWriter SW = new StreamWriter( Server.MapPath("./test02.R"), true, Encoding.UTF8);
        SW.WriteLine(str_rcode);
        SW.Close();
        string R_path = Server.MapPath("./test02.R");
        string RO_path = Server.MapPath("./test2.out");
        string str_cmd = "C: \n" +
                         "cd C:\\Program Files\\R\\R-3.2.1\\bin \n" +
                         "Rscript  --no-restore --no-save " + R_path + " " + RO_path + "  \n" +
                         "pause()";
        cmdShellFile(str_cmd, Server.MapPath("./"), "cmd.bat", "");
        Thread.Sleep(5000);
        if (File.Exists(Server.MapPath(@".\result\DataPic.png")))
        {
            Image2.Visible = true;
            Image2.ImageUrl = "./result/DataPic.png";
        }
        LinkButton2.Visible = true;
    }

    #region ZIP
    //讀取目錄下所有檔案
    private static ArrayList GetFiles(string path)
    {
        ArrayList files = new ArrayList();

        if (Directory.Exists(path))
        {
            files.AddRange(Directory.GetFiles(path));
        }

        return files;
    }

    
    //建立目錄
    private static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
    private void ZipFiles(string path, string password, string comment)
    {
        string zipPath = path + @"\" + Path.GetFileName(path) + ".zip";
        ZipFile zip = new ZipFile();
        if (password != null && password != string.Empty) zip.Password = password;
        if (comment != null && comment != "") zip.Comment = comment;
        ArrayList files = GetFiles(path);
        foreach (string f in files)
        {
            zip.AddFile(f, string.Empty);//第二個參數設為空值表示壓縮檔案時不將檔案路徑加入
        }
        zip.Save(zipPath);
    }
    #endregion
    private void DeleteFile()
    {
        if (File.Exists(Server.MapPath(@".\result\data.csv")))
        {
            File.Delete(Server.MapPath(@".\result\data.csv"));
        }
        if (File.Exists(Server.MapPath(@".\result\DataPic.png")))
        {
            File.Delete(Server.MapPath(@".\result\DataPic.png"));
        }
        if (File.Exists(Server.MapPath(@".\result\result.zip")))
        {
            File.Delete(Server.MapPath(@".\result\result.zip"));
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        ZipFiles(Server.MapPath(".\\result"), string.Empty, string.Empty);
        DownLoadFile("./result/result.zip");
    }

    private void textchg()
    {
        string str_rcode = "test <- read.csv(\"" + Server.MapPath("./test.csv ") + "\")\n " +
                          TextBox1.Text + "\n " +
                          TextBox_w1.Text + TextBox_w2.Text + ", file =\"" + Server.MapPath("./result/data.csv ") + "\"," + TextBox_w4.Text + TextBox_w5.Text + "\n" +
            //"write(t(bb), file = \"" + Server.MapPath("./result/data.csv ")+"\",ncolumns=ncol(bb), sep = \",\") \n "+
                           TextBox5.Text + "\n " +
                           "png(\"" + Server.MapPath("./result/DataPic.png ") + "\")" + "\n " +
                           TextBox4.Text + "\n " +
                           TextBox7.Text;
        str_rcode = str_rcode.Replace(@"\", @"/");
        TextBox_result.Text = str_rcode;

    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        textchg();
    }


}