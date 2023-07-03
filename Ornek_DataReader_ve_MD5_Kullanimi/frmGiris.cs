using System;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Ornek_DataReader_ve_MD5_Kullanimi
{
    public partial class frmGiris : Form
    {
        public frmGiris()
        {
            InitializeComponent();
        }

        public string MD5sifrele(string Veri)
        {
            byte[] ByteData = Encoding.ASCII.GetBytes(Veri);
            MD5 oMd5 = MD5.Create(); //MD5 nesnesini oluşturduk
            byte[] HashData = oMd5.ComputeHash(ByteData); //Hash değeri hesaplanıyor..
            StringBuilder oSb = new StringBuilder();
            for (int x = 0; x < HashData.Length; x++)
            {
                oSb.Append(HashData[x].ToString("x2"));
            }

            return oSb.ToString();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader dr;

            string yol = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + @"\Data.accdb";
            conn.ConnectionString = yol;
            string sifre = MD5sifrele(txtSifre.Text);

            cmd.Connection = conn;
            cmd.CommandText = "select * from kullanici where kadi=@kadi and sifre=@sifre";
            cmd.Parameters.Add("@kadi", OleDbType.VarChar).Value = txtKadi.Text;
            cmd.Parameters.Add("@sifre", OleDbType.VarChar).Value = sifre;

            conn.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                frmAna frm = new frmAna();

                frm.id = dr["kadi"].ToString();

                frm.Show();
                this.Hide();
            }

            else MessageBox.Show("Yanlış");

            conn.Close();
        }
    }
}
