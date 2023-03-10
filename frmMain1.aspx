<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMain1.aspx.cs" Inherits="frmMain1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FORM MAIN</title>
</head>
<body style="background-color: lemonchiffon">
    <link href="CSS/all.min.css" rel="stylesheet" />
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="CSS/jquery-ui.css.css" rel="stylesheet" />
    <form id="form1" runat="server" class="container">
        <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>
        <div style="text-align: center; background-color: blanchedalmond">
            <asp:Label ID="lbHead" runat="server" Text="QMB GLUE STOCK SORT (F4)" Style="font-size: 30px"></asp:Label></div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="form-group" style="text-align:right">
                    <asp:Button ID="btnLogOut" runat="server" Text="Log out" class="btn btn-danger" OnClientClick="ConfirmDialog('Would you like to log out this page?')"/>
                </div>
                <div style="display:none">
                    <asp:Button ID="btnClickLogout" runat="server" OnClick="btnClickLogout_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="form-group">
            <asp:Label ID="lbModel" runat="server" Text="Model: "></asp:Label>
            <asp:DropDownList ID="ddlModel" runat="server" class="form-control" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:Button ID="btnQuery" runat="server" Text="Query" Style="text-align:center" class="btn btn-primary" OnClick="btnQuery_Click" />
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" Style="text-align: center" class="btn btn-success" OnClick="btnConfirm_Click" />
        </div>
        <div class="form-group">
            <table class="table">
                <tr>
                    <th>
                        <p>Model</p>
                    </th>
                    <th>
                        <p>ICE_Box</p>
                    </th>
                    <th>
                        <p>ICE_Block</p>
                    </th>
                    <th>
                        <p>ICE_Tag</p>
                    </th>
                    <th>
                        <p>ICE_Order</p>
                    </th>
                    <th>
                        <p>SN</p>
                    </th>
                    <th>
                        <p>UID</p>
                    </th>
                    <th>
                        <p>StockTime</p>
                    </th>
                    <th>
                        <p>GroupName</p>
                    </th>
                </tr>
                <asp:Repeater ID="rptLoad" runat="server" OnItemDataBound="rptLoad_ItemDataBound">
                    <ItemTemplate>
                        <%--Model	ICE_Box	ICE_Block	ICE_Tag	ICE_Order	SN	UID	StockTime	GroupName--%>
                        <tr>
                            <td>
                                <asp:Label ID="lbModel" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbICE_Box" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbICE_Block" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbICE_Tag" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbICE_Order" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbSN" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbUID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbStockTime" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbGroupName" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="form-group">
            <table class="table">
                <tr>
                    <td>
                        <asp:LinkButton ID="lbFirst" runat="server"
                            OnClick="lbFirst_Click">First</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbPrevious" runat="server"
                            OnClick="lbPrevious_Click">Previous</asp:LinkButton>
                    </td>
                    <td>
                        <asp:DataList ID="rptPaging" runat="server"
                            OnItemCommand="rptPaging_ItemCommand"
                            OnItemDataBound="rptPaging_ItemDataBound"
                            RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbPaging" runat="server"
                                    CommandArgument='<%# Eval("PageIndex") %>'
                                    CommandName="newPage"
                                    Text='<%# Eval("PageText") %> ' Width="50px" style="text-align:center" >
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbNext" runat="server"
                            OnClick="lbNext_Click">Next</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbLast" runat="server"
                            OnClick="lbLast_Click">Last</asp:LinkButton>
                    </td>
                    <td>
                        <asp:Label ID="lblpage" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script src="JS/jquery-3.6.0.min.js"></script>
    <script src="JS/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {            
        })        

        function ConfirmDialog(message) {
            $('<div></div>').appendTo('body')
              .html('<div><h6>' + message + '</h6></div>')
              .dialog({
                  modal: true,
                  title: 'Warning!',
                  zIndex: 10000,
                  autoOpen: true,
                  width: 'auto',
                  resizable: false,
                  buttons: {
                      Yes: function () {
                          // $(obj).removeAttr('onclick');                                
                          // $(obj).parents('.Parent').remove();                          
                          //$('body').append('<h1>Confirm Dialog Result: <i>Yes</i></h1>');

                          //Call Logout click event
                          $("#btnClickLogout").click();

                          $(this).dialog("close");
                      },
                      No: function () {
                          //$('body').append('<h1>Confirm Dialog Result: <i>No</i></h1>');

                          $(this).dialog("close");
                      }
                  },
                  close: function (event, ui) {
                      $(this).remove();
                  }
              });
        };
               
    </script>
</body>
</html>
