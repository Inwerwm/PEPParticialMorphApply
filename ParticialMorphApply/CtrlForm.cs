using PEPlugin;
using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParticialMorphApply
{
    public partial class CtrlForm : Form
    {
        IPERunArgs args;
        IPXPmx pmx;

        TreeNode nodeBrow;
        TreeNode nodeEye;
        TreeNode nodeLip;
        TreeNode nodeOther;
        private float ratio;
        private int selectedMorphID;

        private int SelectedMorphID
        {
            get => selectedMorphID;
            set
            {
                selectedMorphID = value;
                args.Host.Connector.View.TransformView.SelectedMorphIndex = selectedMorphID;
            }
        }
        private float Ratio
        {
            get => ratio;
            set
            {
                ratio = value;
                if (SelectedMorphID != -1)
                {
                    args.Host.Connector.View.TransformView.SelectedMorphIndex = SelectedMorphID;
                    args.Host.Connector.View.TransformView.MorphValue = ratio;
                }
            }
        }

        public CtrlForm(IPERunArgs input)
        {
            InitializeComponent();
            args = input;
            Format();

            nodeBrow = new TreeNode("眉");
            nodeEye = new TreeNode("目");
            nodeLip = new TreeNode("口");
            nodeOther = new TreeNode("他");
            treeViewMorph.Nodes.AddRange(new TreeNode[] { nodeBrow, nodeEye, nodeLip, nodeOther });

            SelectedMorphID = -1;
        }

        public void Format()
        {
            pmx = args.Host.Connector.Pmx.GetCurrentState();
            makeMorphTree();
        }

        private void makeMorphTree()
        {
            nodeEye.Nodes.Clear();
            nodeLip.Nodes.Clear();
            nodeBrow.Nodes.Clear();
            nodeOther.Nodes.Clear();

            // 頂点モーフを分類して木構造に追加
            foreach (var m in pmx.Morph.Where(m => m.Kind == MorphKind.Vertex))
            {
                var n = new TreeNode(m.Name);
                n.Tag = m;

                switch (m.Panel)
                {
                    case 1:
                        nodeBrow.Nodes.Add(n);
                        break;
                    case 2:
                        nodeEye.Nodes.Add(n);
                        break;
                    case 3:
                        nodeLip.Nodes.Add(n);
                        break;
                    case 4:
                        nodeOther.Nodes.Add(n);
                        break;
                    default:
                        break;
                }
            }
        }

        private void CtrlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Visible = false;
            }
        }

        private void textBoxMorphRatio_TextChanged(object sender, EventArgs e)
        {
            Ratio = float.Parse(textBoxMorphRatio.Text);
            trackBarMorphRatio.Value = (int)(Ratio * 1000);
        }

        private void trackBarMorphRatio_Scroll(object sender, EventArgs e)
        {
            Ratio = trackBarMorphRatio.Value / 1000.0f;
            textBoxMorphRatio.Text = Ratio.ToString("F3");
        }

        private void treeViewMorph_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedMorphID = treeViewMorph.SelectedNode.Level < 2 ? -1 : pmx.Morph.IndexOf((IPXMorph)treeViewMorph.SelectedNode.Tag);
        }
    }
}