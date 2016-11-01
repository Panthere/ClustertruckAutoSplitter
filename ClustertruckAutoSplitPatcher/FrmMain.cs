using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CTPatcher
{
    public partial class FrmMain : Form
    {
        Patcher controller;
        PatchSettings ps = new PatchSettings();
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.RestoreDirectory = true;
                ofd.Filter = "Assembly-CSharp Files|*.dll";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        txtInstall.Text = ofd.FileName;
                        // force update
                        ps.dllPath = ofd.FileName;
                        controller.pSettings = ps;

                        
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        throw;
#endif
                        MessageBox.Show("Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnPatch_Click(object sender, EventArgs e)
        {
            try
            {
                ps.isByLevel = rbByLevel.Checked;
                ps.pipeName = txtPipeName.Text.IsEmpty() ? "LiveSplit" : txtPipeName.Text;
                ps.sleepTime = txtSleepMax.Text.ToInt();

                controller.pSettings = ps;

                controller.Patch();

                controller.Write();

                MessageBox.Show("Successfully Patched!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //System.Diagnostics.Process.Start(Application.ExecutablePath);
                //Environment.Exit(0);
            }
            catch (Exception ex)
            {
#if DEBUG
                throw;
#endif
                MessageBox.Show("Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (!controller.IsPatched)
                {
                    MessageBox.Show("You have yet to patch the game!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                controller.Unpatch();


                if (controller.RestoreBackup())
                {
                    MessageBox.Show("Successfully Restored!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Could not restore some files, please reverify your game files to redownload!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //System.Diagnostics.Process.Start(Application.ExecutablePath);
                //Environment.Exit(0);
            }
            catch (Exception ex)
            {
#if DEBUG
                throw;
#endif
                MessageBox.Show("Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            Cleanup();

            try
            {
                //ps.dllPath = @"D:\Steam Games\steamapps\common\ClusterTruck\Clustertruck_Data\Managed\Assembly-CSharp.dll";
                
                ps.typeName = "ClustertruckSplit.Main";
                ps.methodName = "LoadSplitter";


                controller = new Patcher(ps);

                txtInstall.Text = Properties.Settings.Default.installDir;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void txtInstall_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.installDir = txtInstall.Text;
            Properties.Settings.Default.Save();

            if (File.Exists(txtInstall.Text))
            {
                try
                {
                    // force update
                    ps.dllPath = txtInstall.Text;
                    controller.pSettings = ps;

                    
                }
                catch (Exception ex)
                {
#if DEBUG
                    throw;
#endif
                    MessageBox.Show("Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }













        private void Cleanup()
        {
            try
            {
                foreach (string file in Directory.GetFiles(Environment.CurrentDirectory + "\\Data"))
                {
                    try
                    {
                        if (Path.GetFileName(file).StartsWith("DEL_"))
                        {
                            File.Delete(file);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
