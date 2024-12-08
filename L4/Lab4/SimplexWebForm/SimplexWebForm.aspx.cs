using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimplexWebForm
{
    public partial class SimplexWebForm : System.Web.UI.Page
    {
        private Simplex _client;

        protected void Page_Load(object sender, EventArgs e)
        {
            _client = new Simplex();
        }

        protected void Sum_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(x.Text.ToString(), out int xVal))
                {
                    throw new ArgumentException("x is invalid");
                }

                if (!int.TryParse(y.Text.ToString(), out int yVal))
                {
                    throw new ArgumentException("y is invalid");
                }

                var sum = _client.Add(xVal, yVal);
                result.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                Response.Write(
                    $"<script>alert('Error: {ex.Message.Replace("'", "\\'")}')</script>"
                );
            }
        }
    }
}
