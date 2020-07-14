using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing;

public partial class _Login : System.Web.UI.Page
{
    
    protected void Page_init(object sender, EventArgs e)
    {


    }


    protected void Page_Load(object sender, EventArgs e)
    {
        loginButton.Enabled = false;
    }

    protected void Login_click(object sender, EventArgs e)
    {
        if (editorName.Text=="admin" && editorPassword.Text=="telem")
        {
            Session["editorName"] = editorName.Text;
            Response.Redirect("Editor.aspx");
        }
        else
        {
            tryAgain.Text = "*שם המשתמש או הסיסמה אינם נכונים*";
        }
    }
    

}

