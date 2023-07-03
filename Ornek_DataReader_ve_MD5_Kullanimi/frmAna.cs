using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ornek_DataReader_ve_MD5_Kullanimi
{
    public partial class frmAna : Form
    {
        public frmAna()
        {
            InitializeComponent();
        }

        public string id;
        int mesajID = -1;

        OleDbConnection conn = new OleDbConnection();
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataTable dt = new DataTable();

        string yol = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + @"\Data.accdb";

        private void frmAna_Load(object sender, EventArgs e)
        {

            conn.ConnectionString = yol;
            cmd.Connection = conn;
            cmd.CommandText = "select mesajID,kimdenID,mesaj from mesaj where kimeID=@id and silindi=0";
            cmd.Parameters.Add("@id", OleDbType.VarChar).Value = id;

            da.SelectCommand = cmd;
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (mesajID != -1)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "update mesaj set silindi=1 where mesajID=@mesajID";
                cmd.Parameters.Add("@mesajID", OleDbType.Integer).Value = mesajID;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                dt.Rows.Clear();
                cmd.Parameters.Clear();
                cmd.CommandText = "select mesajID,kimdenID,mesaj from mesaj where kimeID=@id and silindi=0";
                cmd.Parameters.Add("@id", OleDbType.VarChar).Value = id;

                da.SelectCommand = cmd;
                da.Fill(dt);

                MessageBox.Show("Mesaj Silindi");

            }
            else MessageBox.Show("Bir Mesaj Seçin");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            mesajID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
        }
    }
}
