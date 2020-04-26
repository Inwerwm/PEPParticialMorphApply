using PEPlugin;
using PEPlugin.Pmx;
using System;
using System.Data;
using System.Linq;
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

        private int SelectedMorphID { get; set; }
        private float Ratio { get; set; }

        public CtrlForm(IPERunArgs input)
        {
            InitializeComponent();
            args = input;
            nodeBrow = new TreeNode("眉");
            nodeBrow.Name = "眉";
            nodeEye = new TreeNode("目");
            nodeEye.Name = "目";
            nodeLip = new TreeNode("口");
            nodeLip.Name = "口";
            nodeOther = new TreeNode("他");
            nodeOther.Name = "他";
            treeViewMorph.Nodes.AddRange(new TreeNode[] { nodeBrow, nodeEye, nodeLip, nodeOther });
            Format();

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
                n.Name = m.Name;
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

        private TreeNode FindNodeByID(int id)
        {
            if (!id.IsWithin(0, pmx.Morph.Count - 1))
                return null;

            var m = pmx.Morph[id];
            if (m.Kind != MorphKind.Vertex)
                return null;
            
            TreeNode n;
            switch (m.Panel)
            {
                case 1:
                    n = nodeBrow.Nodes.Find(m.Name, false)[0];
                    break;
                case 2:
                    n = nodeEye.Nodes.Find(m.Name, false)[0];
                    break;
                case 3:
                    n = nodeLip.Nodes.Find(m.Name, false)[0];
                    break;
                case 4:
                    n = nodeOther.Nodes.Find(m.Name, false)[0];
                    break;
                default:
                    n = null;
                    break;
            }

            return n;
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
            float num;
            if (float.TryParse(textBoxMorphRatio.Text, out num))
            {
                Ratio = num;
                trackBarMorphRatio.Value = Math.Max(Math.Min((int)(Ratio * 1000), trackBarMorphRatio.Maximum), 0);
            }
        }

        private void trackBarMorphRatio_Scroll(object sender, EventArgs e)
        {
            Ratio = trackBarMorphRatio.Value / 1000.0f;
            textBoxMorphRatio.Text = Ratio.ToString("F3");
        }

        private void treeViewMorph_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBoxSelectedNodeName.Text = treeViewMorph.SelectedNode.Level == 1 ? treeViewMorph.SelectedNode.Name : "";
            SelectedMorphID = treeViewMorph.SelectedNode.Level < 1 ? -1 : pmx.Morph.IndexOf((IPXMorph)treeViewMorph.SelectedNode.Tag);
            textBoxMorphRatio.Text = Ratio.ToString("F3");
            trackBarMorphRatio.Value = Math.Max(Math.Min((int)(Ratio * 1000), trackBarMorphRatio.Maximum), 0);
        }

        private void CtrlForm_Activated(object sender, EventArgs e)
        {
            /*
            args.Host.Connector.View.TransformView.MorphChecker = true;
            SelectedMorphID = args.Host.Connector.View.TransformView.SelectedMorphIndex;
            treeViewMorph.SelectedNode = FindNodeByID(SelectedMorphID);
            if (treeViewMorph.SelectedNode == null)
            {
                SelectedMorphID = -1;
                textBoxSelectedNodeName.Text = "";
            }

            Ratio = treeViewMorph.SelectedNode == null ? 0 : args.Host.Connector.View.TransformView.MorphValue;
            textBoxMorphRatio.Text = Ratio.ToString("F3");
            trackBarMorphRatio.Value = Math.Max(Math.Min((int)(Ratio * 1000), trackBarMorphRatio.Maximum), 0);
            */
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            Format();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            if (args.Host.Connector.Form.SelectedExpressionIndex < 0)
            {
                MessageBox.Show("フォームから追加先の頂点モーフを選択してください。");
                return;
            }
            if(SelectedMorphID < 0)
            {
                MessageBox.Show($"追加元頂点モーフが選択されていません。{Environment.NewLine}プラグインのウィンドウ左側のツリービューからモーフを選択してください。{Environment.NewLine}選択しているにも関わらずこのエラーが表示されている場合は再読込してから再びモーフを選択してください。");
            }
            pmx = args.Host.Connector.Pmx.GetCurrentState();
            var selectedVertices = args.Host.Connector.View.PmxView.GetSelectedVertexIndices().Select(i => pmx.Vertex[i]).ToArray();
            var selectedMorph = pmx.Morph[args.Host.Connector.Form.SelectedExpressionIndex];
            if (selectedMorph.Kind != MorphKind.Vertex)
            {
                MessageBox.Show($"{selectedMorph.Name}は頂点モーフではありません。{Environment.NewLine}頂点モーフを選択してください。");
                return;
            }

            var offsets = pmx.Morph[SelectedMorphID].Offsets.Select(o => o as IPXVertexMorphOffset).ToArray();

            foreach (var o in offsets)
            {
                // オフセットの頂点が選択頂点に含まれていなければ
                if (!selectedVertices.Contains(o.Vertex))
                    continue;
                var newOffset = PEStaticBuilder.Pmx.VertexMorphOffset();
                newOffset.Vertex = o.Vertex;
                newOffset.Offset = o.Offset * Ratio;
                selectedMorph.Offsets.Add(newOffset);
                //o.Vertex.Position += o.Offset * Ratio;
            }

            Utility.Update(args.Host.Connector, pmx, PmxUpdateObject.Morph);
            MessageBox.Show("完了");
        }
    }
}