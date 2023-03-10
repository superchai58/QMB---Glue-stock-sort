<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLogin1.aspx.cs" Inherits="frmLogin1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FORM LOGIN</title>
</head>
<body style="background-color:lemonchiffon">
    <link href="CSS/all.min.css" rel="stylesheet" />
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <style>
    .container i {
        margin-left: -30px;
        cursor: pointer;
    }
    </style>
    <form id="form1" runat="server" class="container">
    <div style="text-align:center; background-color: blanchedalmond"><asp:Label ID="lbHead" runat="server" Text="QMB GLUE STOCK SORT (F4)" style="font-size:30px"></asp:Label></div>   
    <br />
    <div>
        <div class="form-group">
            <asp:Label ID="lbID" runat="server" Text="ID: "></asp:Label>
            <asp:TextBox ID="txtID" runat="server" placeholder="ID" class="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label ID="lbPass" runat="server" Text="Pass: "></asp:Label>                    
            <asp:TextBox ID="txtPass" placeholder="Password" TextMode="Password" runat="server" class="form-control"></asp:TextBox>       
            <asp:CheckBox ID="chkPass" runat="server" Text="Show Password" />                                 
            <br />
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" class="btn btn-success" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" class="btn btn-primary" />
        </div>
    </div>
    </form>
    <script src="JS/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //--txtPass enter login button click--
            $("#txtPass").keyup(function (event) {
                if (event.keyCode === 13) {
                    $("#btnLogin").click();
                }
            });

            //--Click show password--
            $('#chkPass').click(function () {
                $('#txtPass').attr('type', $(this).is(':checked') ? 'text' : 'password');
            });
        })
               
    </script>
</body>
</html>
