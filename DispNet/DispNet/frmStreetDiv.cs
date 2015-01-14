using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DispNet
{
    public partial class frmStreetDiv : Form
    {
        public string strStreetId;
        DataSet dsStreet = new DataSet();
        BindingSource bsStreet = new BindingSource();

        public frmStreetDiv()
        {
            InitializeComponent();
        }

        private void frmStreetDiv_Load(object sender, EventArgs e)
        {
            string strSqlTextStreet = "SELECT StreetDivide.StreetId, Street.StreetName, StreetType.StreetTypeName, " +
                                      "StreetDivide.DivideHomeFr, StreetDivide.DivideHomeLs, Unit.UnitName, Unit.UnitId " +
                                      "FROM StreetDivide INNER JOIN Street ON StreetDivide.StreetId = Street.StreetId " +
                                      "INNER JOIN Unit ON StreetDivide.UnitId = Unit.UnitId " +
                                      "INNER JOIN StreetType ON Street.StreetTypeId = StreetType.StreetTypeId " +
                                      "WHERE (StreetDivide.StreetId = ";
            strSqlTextStreet = strSqlTextStreet + strStreetId + ") ORDER BY StreetDivide.DivideHomeFr";
            SqlDataAdapter daStreet = new SqlDataAdapter(strSqlTextStreet, FrmPermit.Instance.cnn);
            daStreet.Fill(dsStreet, "StreetDivide");
            bsStreet.DataSource = dsStreet;
            bsStreet.DataMember = "StreetDivide";

            c1ListStreetDiv.DataSource = bsStreet;
            c1ListStreetDiv.DataMember = "StreetDivide";

            c1ListStreetDiv.Columns.RemoveAt(0);
            c1ListStreetDiv.Columns.RemoveAt(5);
            c1ListStreetDiv.Splits[0].DisplayColumns[0].Width = 80;
            c1ListStreetDiv.Splits[0].DisplayColumns[1].Width = 80;
            c1ListStreetDiv.Splits[0].DisplayColumns[2].Width = 50;
            c1ListStreetDiv.Splits[0].DisplayColumns[3].Width = 50;
            c1ListStreetDiv.Splits[0].DisplayColumns[4].Width = 80;

            c1ListStreetDiv.Columns[0].Caption = "Улица";
            c1ListStreetDiv.Columns[1].Caption = "Тип";
            c1ListStreetDiv.Columns[2].Caption = "От";
            c1ListStreetDiv.Columns[3].Caption = "До";
            c1ListStreetDiv.Columns[4].Caption = "П-Часть";

            c1ListStreetDiv.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1ListStreetDiv.Splits[0].DisplayColumns[1].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1ListStreetDiv.Splits[0].DisplayColumns[2].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1ListStreetDiv.Splits[0].DisplayColumns[3].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1ListStreetDiv.Splits[0].DisplayColumns[4].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            //c1ListStreetDiv.SetSelected(0, true);
        }

        private void RecStreetDivUnit()
        {
            DataRow drStreet;
            drStreet = dsStreet.Tables["StreetDivide"].Rows[bsStreet.Position];
            FrmPermit.Instance.m_iStreetDivUnit = int.Parse(drStreet["UnitId"].ToString());
            Close();
        }

        private void btnRecStreet_Click(object sender, EventArgs e)
        {
            RecStreetDivUnit();
        }

        private void c1ListStreetDiv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                RecStreetDivUnit();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}