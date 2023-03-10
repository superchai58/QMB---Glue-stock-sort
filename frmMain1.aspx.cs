using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmMain1 : System.Web.UI.Page
{
    ConnectDBSMT oConSMT = new ConnectDBSMT();
    ModelInfo oModel = new ModelInfo();
    List<ModelInfo> listModel = new List<ModelInfo>();
    string strSelect = "";
    readonly PagedDataSource _pgsource = new PagedDataSource();
    DataSet ds = new DataSet();
    private int _pageSize = 10;
    int _firstIndex, _lastIndex;
    List<DataGridInfo> listDataGridInfo = new List<DataGridInfo>();
    DataGridInfo oDtataGridInfo = new DataGridInfo();

    public List<DataGridInfo> JobSeekersList
    {
        get
        {
            // check if not exist to make new (normally before the post back)
            // and at the same time check that you did not use the same viewstate for other object
            if (!(ViewState["listDataGridInfo"] is List<DataGridInfo>))
            {
                // need to fix the memory and added to viewstate
                ViewState["listDataGridInfo"] = new List<DataGridInfo>();
            }

            return (List<DataGridInfo>)ViewState["listDataGridInfo"];
        }
        set
        {
            ViewState["listDataGridInfo"] = value;
        }
    }

    private int CurrentPage
    {
        get
        {
            if (ViewState["CurrentPage"] == null)
            {
                return 0;
            }
            return ((int)ViewState["CurrentPage"]);
        }
        set
        {
            ViewState["CurrentPage"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {        
        string uid = "";
        uid = Request.QueryString["UID"];

        if (uid == null)
        {
            Response.Redirect("frmLogin1.aspx");
        }
        if (!IsPostBack)
        {
            //--Get model from GLUE_STOCK--
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select distinct Model From GLUE_STOCK with(nolock)";
            cmd.CommandTimeout = 180;
            DataTable dt = oConSMT.Query(cmd);

            //--List model--
            listModel = new List<ModelInfo>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    oModel = new ModelInfo();
                    oModel.StrModel = row["Model"].ToString().Trim();

                    listModel.Add(oModel);
                }
                ddlModel.DataSource = listModel;
                ddlModel.DataValueField = "strModel"; //The Value of the DropDownList, to get it you should call ddlDepartments.SelectedValue;
                ddlModel.DataTextField = "strModel"; //The Name shown of the DropDownList.
                ddlModel.DataBind();
            }
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        //--Sort data by model--
        try
        {
            strSelect = ddlModel.SelectedValue.ToString().Trim();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "exec Glue_StockSort '" + strSelect + "', 'Update'";
            cmd.CommandTimeout = 180;

            ds = oConSMT.QueryDataSet(cmd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string strTmp = ds.Tables[0].Rows[0]["Msg"].ToString().Trim();
                if (strTmp == "Pass")
                {
                    listDataGridInfo = new List<DataGridInfo>();
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        oDtataGridInfo = new DataGridInfo();
                        oDtataGridInfo.GroupName1 = row["GroupName"].ToString().Trim();
                        oDtataGridInfo.ICE_Block1 = Convert.ToInt32(row["ICE_Block"].ToString().Trim());
                        oDtataGridInfo.ICE_Box1 = row["ICE_Box"].ToString().Trim();
                        oDtataGridInfo.ICE_Order1 = Convert.ToInt32(row["ICE_Order"].ToString().Trim());
                        oDtataGridInfo.ICE_Tag1 = Convert.ToInt32(row["ICE_Tag"].ToString().Trim());
                        oDtataGridInfo.Model1 = row["Model"].ToString().Trim();
                        oDtataGridInfo.SN1 = row["SN"].ToString().Trim();
                        oDtataGridInfo.StockTime1 = row["StockTime"].ToString().Trim();
                        oDtataGridInfo.UID1 = row["UID"].ToString().Trim();

                        listDataGridInfo.Add(oDtataGridInfo);
                    }
                    JobSeekersList = listDataGridInfo;
                    ClientScript.RegisterStartupScript(this.GetType(), "INFORMATION!", "alert('Sort Model: " + strSelect + " OK!.');", true);                    
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ERROR!", "alert('An error has occurred, please contact QMS.');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ERROR!", "alert('An error has occurred, please contact QMS.');", true);
            }
            BindDataIntoRepeater();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "EXCEPTION!", "alert('Exception: " + ex + ".');", true);
        }
    }

    protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ViewState["strSelect"] = ddlModel.SelectedValue.ToString().Trim();
        strSelect = ddlModel.SelectedValue.ToString().Trim();
    }

    protected void rptLoad_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //--Set find control--

            //Model ICE_Box ICE_Block ICE_Tag ICE_Order SN  UID StockTime   GroupName
            Label lbModel = e.Item.FindControl("lbModel") as Label;
            Label lbICE_Box = e.Item.FindControl("lbICE_Box") as Label;
            Label lbICE_Block = e.Item.FindControl("lbICE_Block") as Label;
            Label lbICE_Tag = e.Item.FindControl("lbICE_Tag") as Label;
            Label ICE_Order = e.Item.FindControl("lbICE_Order") as Label;
            Label lbSN = e.Item.FindControl("lbSN") as Label;
            Label lbUID = e.Item.FindControl("lbUID") as Label;
            Label lbStockTime = e.Item.FindControl("lbStockTime") as Label;
            Label lbGroupName = e.Item.FindControl("lbGroupName") as Label;

            ////--Set Model--
            DataGridInfo oDataGridInfo = e.Item.DataItem as DataGridInfo;
            lbModel.Text = oDataGridInfo.Model1.ToString().Trim();
            lbICE_Box.Text = oDataGridInfo.ICE_Box1.ToString().Trim();
            lbICE_Block.Text = oDataGridInfo.ICE_Block1.ToString().Trim();
            lbICE_Tag.Text = oDataGridInfo.ICE_Tag1.ToString().Trim();
            ICE_Order.Text = oDataGridInfo.ICE_Order1.ToString().Trim();
            lbSN.Text = oDataGridInfo.SN1.ToString().Trim();
            lbUID.Text = oDataGridInfo.UID1.ToString().Trim();
            lbStockTime.Text = oDataGridInfo.StockTime1.ToString().Trim();
            lbGroupName.Text = oDataGridInfo.GroupName1.ToString().Trim();            
        }
    }

    private void BindDataIntoRepeater()
    {        
        _pgsource.DataSource = JobSeekersList;
        _pgsource.AllowPaging = true;
        // Number of items to be displayed in the Repeater
        _pgsource.PageSize = _pageSize;
        _pgsource.CurrentPageIndex = CurrentPage;
        // Keep the Total pages in View State
        ViewState["TotalPages"] = _pgsource.PageCount;
        // Example: "Page 1 of 10"
        lblpage.Text = "Page " + (CurrentPage + 1) + " of " + _pgsource.PageCount;
        // Enable First, Last, Previous, Next buttons
        lbPrevious.Enabled = !_pgsource.IsFirstPage;
        lbNext.Enabled = !_pgsource.IsLastPage;
        lbFirst.Enabled = !_pgsource.IsFirstPage;
        lbLast.Enabled = !_pgsource.IsLastPage;

        // Bind data into repeater
        rptLoad.DataSource = _pgsource;
        rptLoad.DataBind();

        // Call the function to do paging
        HandlePaging();
        //rptLoad.DataSource = listDataGridInfo;
        //rptLoad.DataBind();
    }

    private void HandlePaging()
    {
        var dt = new DataTable();
        dt.Columns.Add("PageIndex"); //Start from 0
        dt.Columns.Add("PageText"); //Start from 1

        _firstIndex = CurrentPage - 5;
        if (CurrentPage > 5)
            _lastIndex = CurrentPage + 5;
        else
            _lastIndex = 10;

        // Check last page is greater than total page then reduced it 
        // to total no. of page is last index
        if (_lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
        {
            _lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
            _firstIndex = _lastIndex - 10;
        }

        if (_firstIndex < 0)
            _firstIndex = 0;

        // Now creating page number based on above first and last page index
        for (var i = _firstIndex; i < _lastIndex; i++)
        {
            var dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i + 1;
            dt.Rows.Add(dr);
        }

        rptPaging.DataSource = dt;
        rptPaging.DataBind();
    }

    protected void lbFirst_Click(object sender, EventArgs e)
    {
        CurrentPage = 0;
        BindDataIntoRepeater();
    }
    protected void lbLast_Click(object sender, EventArgs e)
    {
        CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
        BindDataIntoRepeater();
    }
    protected void lbPrevious_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        BindDataIntoRepeater();
    }
    protected void lbNext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        BindDataIntoRepeater();
    }

    protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (!e.CommandName.Equals("newPage")) return;
        CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
        BindDataIntoRepeater();
    }

    protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        var lnkPage = (LinkButton)e.Item.FindControl("lbPaging");
        if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
        lnkPage.Enabled = false;
        lnkPage.BackColor = Color.FromName("#00FF00");
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        //--Query GLUE_Stock--
        try
        {
            strSelect = ddlModel.SelectedValue.ToString().Trim();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "exec Glue_StockSort '" + strSelect + "', 'Query'";
            cmd.CommandTimeout = 180;

            ds = oConSMT.QueryDataSet(cmd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                listDataGridInfo = new List<DataGridInfo>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    oDtataGridInfo = new DataGridInfo();
                    oDtataGridInfo.GroupName1 = row["GroupName"].ToString().Trim();
                    oDtataGridInfo.ICE_Block1 = Convert.ToInt32(row["ICE_Block"].ToString().Trim());
                    oDtataGridInfo.ICE_Box1 = row["ICE_Box"].ToString().Trim();
                    oDtataGridInfo.ICE_Order1 = Convert.ToInt32(row["ICE_Order"].ToString().Trim());
                    oDtataGridInfo.ICE_Tag1 = Convert.ToInt32(row["ICE_Tag"].ToString().Trim());
                    oDtataGridInfo.Model1 = row["Model"].ToString().Trim();
                    oDtataGridInfo.SN1 = row["SN"].ToString().Trim();
                    oDtataGridInfo.StockTime1 = row["StockTime"].ToString().Trim();
                    oDtataGridInfo.UID1 = row["UID"].ToString().Trim();

                    listDataGridInfo.Add(oDtataGridInfo);
                }
                JobSeekersList = listDataGridInfo;                              
            }
            BindDataIntoRepeater();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "EXCEPTION!", "alert('Exception: " + ex + ".');", true);
        }
    }

    protected void btnClickLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmLogin1.aspx");
    }
}