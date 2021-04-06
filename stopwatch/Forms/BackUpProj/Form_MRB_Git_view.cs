using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace stopwatch.Forms.BackUpProj
{
    public partial class Form_MRB_Git_view : Form
    {
        MRB_Git.BackupInfo info;
        MRB_Git git;
        public Form_MRB_Git_view(MRB_Git git, MRB_Git.BackupInfo info)
        {
            InitializeComponent();
            this.info = info;
            this.git = git;
            UpdateTree();
            Text += " - " + info.ProjectSection["code"];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">local_path|server_path</param>
        void AddNode(string[] path)
        {
            var A = path[0].Split('\\');
            var nodes = treeView1.Nodes[0].Nodes;
            for (int i = 0; i < A.Length; i++)
            {
                var found = false;
                foreach (TreeNode n in nodes)
                    if (n.Text == A[i])
                    {
                        nodes = n.Nodes;
                        found = true;
                        break;
                    }
                if (!found)
                {
                    var n = nodes.Add(A[i]);
                    if (i < A.Length - 1)
                        n.SelectedImageKey = n.ImageKey = "folder";
                    else
                    {
                        n.Text = Path.GetFileName(path[0]);
                        n.Tag = server_dir + "files\\" + path[1];
                        n.ToolTipText = n.Tag + "";
                        var ext = Path.GetExtension(A[i]).ToLower();
                        if (!imageList1.Images.ContainsKey(ext))
                        {
                            Icon iconForFile = null;
                            if ((iconForFile = Icon.ExtractAssociatedIcon(n.Tag + "")) != null)
                                imageList1.Images.Add(ext, iconForFile);
                            else
                                ext = "file";
                        }
                        n.SelectedImageKey = n.ImageKey = ext;

                    }
                    nodes = n.Nodes;
                }
            }
        }
        string server_dir = "";
        void UpdateTree()
        {
            server_dir = Path.GetDirectoryName(info.path);
            if (!server_dir.EndsWith("\\")) server_dir += "\\";
            treeView1.Nodes.Clear();
            var n = treeView1.Nodes.Add(info.ProjectSection["code"]);
            if (n.Text + "" == "") n.Text = "Project-BackUp";
            n.ImageKey = n.SelectedImageKey = "folder";
            n.ToolTipText = server_dir;
            var log = File.ReadAllLines(server_dir + "\\" + info.InfoSection["LogFile"]);
            foreach (var f in log)
                if (f.Trim() != "" && !f.StartsWith("#"))
                {
                    AddNode(f.Split('|'));
                }
            n.Expand();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                e.Cancel = true;
                return;
            }
            openToolStripMenuItem.Enabled =
            copyToolStripMenuItem.Enabled =
            viewtextToolStripMenuItem.Enabled =
                treeView1.SelectedNode.Nodes.Count == 0;
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            var i = treeView1.HitTest(e.Location);
            treeView1.SelectedNode = i.Node;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null) return;
            var file = treeView1.SelectedNode.Tag + "";
            if (file.Trim() != "")
                System.Diagnostics.Process.Start(file);
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null) return;
            var file = treeView1.SelectedNode.Tag + "";
            try
            {
                System.Diagnostics.Process.Start("notepad++.exe", file);
            }
            catch
            {
                System.Diagnostics.Process.Start("notepad.exe", file);
            }
        }

        void CopyNodeTo(string dir, TreeNode node, bool overwrite)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            foreach (TreeNode n in node.Nodes)
            {
                if (n.Nodes.Count == 0)
                {
                    if (overwrite)
                        File.Copy(n.Tag + "", dir + n.Text, true);
                    else
                    {
                        if (!File.Exists(dir + n.Text))
                            File.Copy(n.Tag + "", dir + n.Text, false);
                    }
                }
                else
                {
                    dir = dir + n.Text + "\\";
                    CopyNodeTo(dir, n, overwrite);
                }
            }
        }
        private void saveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null) return;
            var file = treeView1.SelectedNode.Tag + "";
            if (treeView1.SelectedNode.Nodes.Count > 0)
            {
                using (var sdg = new FolderBrowserDialog())
                    if (sdg.ShowDialog() == DialogResult.OK)
                    {
                        var dir = sdg.SelectedPath + "\\" + treeView1.SelectedNode.Text + "\\";
                        var overwrite = DialogResult.No;
                        if (Directory.Exists(dir))
                            overwrite = Form_msg.Show(this, "در صورت وجود فایل ها در آدرس انتخاب شده، فایل ها جایگزین شوند؟", btn: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);
                        if (overwrite != DialogResult.Cancel)
                            CopyNodeTo(dir, treeView1.SelectedNode, overwrite == DialogResult.Yes);
                    }
            }
            else
            {
                using (var sdg = new SaveFileDialog()
                {
                    Filter = Path.GetExtension(file) + "|*" + Path.GetExtension(file) + "|All files|*.*",
                    FileName = treeView1.SelectedNode.Text,
                    OverwritePrompt = true
                })
                {
                    if (sdg.ShowDialog() == DialogResult.OK)
                        File.Copy(file, sdg.FileName, true);
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null) return;
            var file = treeView1.SelectedNode.Tag + "";
            var paths = new System.Collections.Specialized.StringCollection();
            paths.Add(file);
            Clipboard.SetFileDropList(paths);
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var file = e.Node.Tag + "";
            System.Diagnostics.Process.Start(file);
        }
    }
}
