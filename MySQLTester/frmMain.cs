using CommonLibrary;

using MySqlConnector;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Net.WebRequestMethods;

namespace MySQLTester
{
    public partial class frmMain : Form
    {
        private string ConnStr
        {
            get
            {
                return $"Server={txtAddr.Text.Trim()};Port={txtPort.Text.Trim()};User ID={txtId.Text.Trim()};Password={txtPw.Text};Database={txtDBName.Text.Trim()};Connection Timeout=2;";
            }
        }
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            dgv.DoubleBuffered(true);
        }
        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ConnectTest();
        }

        List<DateTime> listDt = new List<DateTime>();
        private void btnTest_Click(object sender, EventArgs e)
        {
            ConnectTest();
        }

        private void ConnectTest()
        {
            DateTime dtNow = DateTime.Now;
            listDt.Add(dtNow);
            pnTop.Enabled = false;
            bool isCompleted = false;

            Task openTask = Task.Factory.StartNew(() =>
            {
                DateTime dtTmp = dtNow.DeepClone();
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(ConnStr))
                    {
                        connection.Open();
                        isCompleted = true;
                        MsgBox.ShowInfo("접속 성공!");
                    }
                }
                catch (Exception ex)
                {
                    if (listDt.Contains(dtTmp) == false)
                    {
                        return;
                    }
                    isCompleted = true;
                    MsgBox.ShowError("접속 실패!\n" + ex.Message);
                }
                finally
                {
                    listDt.RemoveAll(x => x == dtTmp);
                    this.InvokeIfRequired(() =>
                    {
                        pnTop.Enabled = true;
                    });
                }
            });

            if (isCompleted)
            {
                this.InvokeIfRequired(() =>
                {
                    pnTop.Enabled = true;
                });
                return;
            }

            var retTask = Task.WaitAny(openTask, Task.Delay(1000 * 1));
            this.InvokeIfRequired(() =>
            {
                pnTop.Enabled = true;
            });

            if (retTask != 0 && isCompleted == false)
            {
                listDt.Remove(dtNow);
                MsgBox.ShowWarning("접속 TimeOut!");
            }
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            RunQuery();
        }


        DataTable dt = new DataTable();
        private void RunQuery()
        {
            DateTime dtNow = DateTime.Now;
            btnRun.Enabled = false;
            txtQuery.Enabled = false;

            Task.Factory.StartNew(() =>
            {
                try
                {
                    if (dt != null)
                    {
                        dt.Clear();
                        dt = null;
                    }
                    dt = GetDataTable(txtQuery.Text);
                    if (recentException != null)
                    {
                        MsgBox.ShowError(recentException.Message);
                        return;
                    }
                    if (dt == null)
                    {
                        return;
                    }

                    this.InvokeIfRequired(() =>
                    {
                        dgv.DataSource = null;
                        dgv.DataSource = dt;

                        lbElapsedTime.Text = $"{DateTime.Now.Subtract(dtNow).TotalSeconds.ToString("#0.000")} Sec";
                        lbTotalRows.Text = "총 " + string.Format("{0:n0}", dt.Rows.Count) + " 행";
                    });
                }
                catch (Exception ex)
                {
                    this.InvokeIfRequired(() =>
                    {
                        lbElapsedTime.Text = "측정 오류";
                    });
                    MsgBox.ShowError("오류!\n" + ex.Message);
                }
                finally
                {
                    this.InvokeIfRequired(() =>
                    {
                        btnRun.Enabled = true;
                        txtQuery.Enabled = true;
                    });
                }
            });
        }

        Exception recentException = null;
        public DataTable GetDataTable(string strQuery, params SqlParameter[] SqlParam)
        {
            try
            {
                recentException = null;
                var table = new DataTable();

                using (MySqlConnection conn = new MySqlConnection(ConnStr))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, conn))
                    {
                        cmd.CommandTimeout = 10;
                        cmd.CommandType = CommandType.Text;
                        foreach (SqlParameter param in SqlParam)
                        {
                            cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                        }
                        using (var r = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            table.Load(r);
                        }
                    }
                }
                return table;
            }
            catch (Exception ex)
            {
                recentException = ex;
                return null;
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                RunQuery();
            }
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
