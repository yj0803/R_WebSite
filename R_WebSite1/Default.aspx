<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #Text1 {
            margin-top: 3px;
            margin-bottom: 30px;
        }
        .auto-style1 {
            color: #000000;
            background-color: #6699FF;
        }
        .auto-style2 {
            background-color: #BEFEA3;
        }
        .auto-style3 {
            font-size: xx-large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="background-color: #FED7FF">
    <div>
    
        <span class="auto-style3"><strong>R Code 執行頁面</strong></span><br />
        <br />
    
        <asp:Label ID="Label5" runat="server" Text="執行預設R Code" style="font-weight: 700"></asp:Label>
        <br />
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="執行預設R Code" />
    
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">下載輸出結果</asp:LinkButton>
    
        <br />
        <asp:Image ID="Image1" runat="server" ImageAlign="TextTop" />
        <br />
        <br />
        <br />
        <br />
        <strong>執行自定義R Code</strong><br />
        (<span class="auto-style1">藍色</span>部分為固定碼 ,不可修改.<span class="auto-style2">綠色</span>部分為自訂碼,可依需求輸入)<br />
        <br />
        <asp:TextBox ID="TextBox2" runat="server" BackColor="#6699FF" ReadOnly="True" Width="500px">test &lt;- 輸入資料</asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="請輸入csv匯出部分程式碼"></asp:Label>
    
    </div>
        <asp:TextBox ID="TextBox1" runat="server" ForeColor="Black" Height="187px" Rows="9" TextMode="MultiLine" Width="500px" AutoPostBack="True" BackColor="#BEFEA3" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <br />
        <asp:Label ID="Label_OutCode1" runat="server" Text="ex:"></asp:Label>
        <br />
        <asp:Label ID="Label_OutCode2" runat="server" Text="aa &lt;- as.data.frame(test)"></asp:Label>
        <br />
        <asp:Label ID="Label_OutCode3" runat="server" Text="bb &lt;-as.matrix(aa)"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label15" runat="server" Text="請輸入匯出CSV參數"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox_w1" runat="server" BackColor="#6699FF" ReadOnly="True" Width="34px">write(</asp:TextBox>
        <asp:TextBox ID="TextBox_w2" runat="server" Width="68px" AutoPostBack="True" BackColor="#BEFEA3" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <asp:TextBox ID="TextBox_w3" runat="server" BackColor="#6699FF" ReadOnly="True">, file = 預設路徑,</asp:TextBox>
        <asp:TextBox ID="TextBox_w4" runat="server" AutoPostBack="True" BackColor="#BEFEA3" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <asp:TextBox ID="TextBox_w5" runat="server" BackColor="#6699FF" ReadOnly="True" Width="82px">, sep = &quot;,&quot;)</asp:TextBox>
        <br />
        ex:<br />
        <asp:Label ID="Label10" runat="server" BackColor="#6699FF" Text="write("></asp:Label>
&nbsp;<asp:Label ID="Label11" runat="server" BackColor="#BEFEA3" Text="t(bb)"></asp:Label>
&nbsp;<asp:Label ID="Label12" runat="server" BackColor="#6699FF" Text=", file = 預設路徑,"></asp:Label>
&nbsp;<asp:Label ID="Label13" runat="server" BackColor="#BEFEA3" Text="ncolumns=ncol(bb)"></asp:Label>
&nbsp;<asp:Label ID="Label14" runat="server" BackColor="#6699FF" Text=", sep = &quot;,&quot;)"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="請輸入欲載入的繪圖Lib"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox5" runat="server" Height="42px" TextMode="MultiLine" Width="500px" AutoPostBack="True" BackColor="#BEFEA3" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <br />
        <asp:Label ID="Label6" runat="server" Text="ex :"></asp:Label>
        <br />
        <asp:Label ID="Label7" runat="server" Text="library('ggplot2')"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="TextBox3" runat="server" BackColor="#6699FF" ReadOnly="True" Width="500px">png(&quot;預設路徑&quot;)</asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="請輸入繪圖部分程式碼"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox4" runat="server" Height="164px" TextMode="MultiLine" Width="500px" AutoPostBack="True" BackColor="#BEFEA3" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <br />
        <asp:Label ID="Label8" runat="server" Text="ex :"></asp:Label>
        <br />
        <asp:Label ID="Label9" runat="server" Text="ggplot(aa,aes(x=TEMP,y=WSP))+geom_point()"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="TextBox7" runat="server" BackColor="#6699FF" ReadOnly="True" style="margin-bottom: 0px" Width="500px">dev.off()</asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label16" runat="server" Text="程式碼預覽"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox_result" runat="server" BackColor="#6699FF" Height="169px" TextMode="MultiLine" Width="500px"></asp:TextBox>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <p>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="執行自定義R Code" />
            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">下載輸出結果</asp:LinkButton>
        </p>
        <p>
            <asp:Image ID="Image2" runat="server" />
        </p>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
