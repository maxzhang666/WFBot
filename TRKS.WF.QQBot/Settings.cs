﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newbe.Mahua.MahuaEvents;
using TRKS.WF.QQBot;

namespace Settings
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Config.Instance.Code = textBox1.Text;
            Config.Save();
            label2.Text = $"当前的口令为:{Config.Instance.Code}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label5.Text = $"当前的口令为:{Config.Instance.Code}";
            if (string.IsNullOrEmpty(Config.Instance.QQ))
            {
                label5.Text = $"当前不发送任何报错.";
            }
            else
            {
                label5.Text = $"当前的QQ为:{Config.Instance.QQ}";
            }
            UpdateCheckBox();
            checkBox9.Checked = Config.Instance.AcceptInvitation;
            checkBox10.Checked = Config.Instance.AcceptJoiningRequest;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void InvasionsCheck(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var rewardList = Config.Instance.InvationRewardList;
            var tags = (string[])checkbox.Tag;
            foreach (var item in tags)
            {
                if (checkbox.Checked)
                {
                    rewardList.Add(item);
                }
                else
                {
                    rewardList.Remove(item);
                }
            }
            Config.Save();
        }
        public void UpdateCheckBox()
        {
            var checkboxes = Controls.OfType<CheckBox>().Where(box => box.Tag is string[]);
            foreach (var checkbox in checkboxes)
            {
                var items = new HashSet<string>(((string[])checkbox.Tag));
                var invRewards = Config.Instance.InvationRewardList;
                checkbox.Checked = items.Intersect(invRewards).Any();
                foreach (var item in items)
                {
                    Config.Instance.InvationRewardList.Remove(item);
                }
 
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var qq = textBox2.Text;
            Config.Instance.QQ = qq;
            Config.Save();
            if (string.IsNullOrEmpty(qq))
            {
                label5.Text = $"当前不发送任何报错.";
            }
            else if (qq.IsNumber())
            {
                label5.Text = $"当前的QQ为: {qq}";
            }
            else
            {
                MessageBox.Show("您的QQ是真的牛逼.");
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            Config.Instance.AcceptInvitation = checkBox9.Checked;
            Config.Save();
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            Config.Instance.AcceptJoiningRequest = checkBox10.Checked;
            Config.Save();
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var group in Config.Instance.WFGroupList)
            {
                Messenger.SendGroup(group, $"[来自管理者]通知:{textBox3.Text}");
                Thread.Sleep(100);
            }
        }
    }

    public static class StringExtensions
    {
        public static bool IsNumber(this string source)
        {
            return int.TryParse(source, out _);
        }
    }
}
