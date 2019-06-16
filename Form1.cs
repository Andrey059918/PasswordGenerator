using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace PasswordGenerator
{
	public partial class Form1 : Form
	{
		MD5Cng md5hasher;
		public Form1()
		{
			InitializeComponent();
			md5hasher = new MD5Cng();
			SetTextBoxEvents(textBox1, "Адрес или название сайта");
			SetTextBoxEvents(textBox2, "Телефон, логин или E-mail");
			SetTextBoxEvents(textBox3, "Контрольное слово или фраза");
			MakeHash();
			textBox1.Select(0,0);
		}


		private void SetTextBoxEvents(TextBox textBox, String defaultText)
		{
			textBox.Text = defaultText;
			textBox.ForeColor = Color.FromArgb(75,75,75);
			textBox.KeyDown += (sender, args) =>
			{
				if (args.KeyCode == Keys.Back)
				{
					if (textBox.TextLength <2)
					{
						textBox.Text = defaultText;
						textBox.ForeColor = Color.FromArgb(75, 75, 75);
					}
				}
				else if (textBox.Text == defaultText)
				{
					textBox.Text = "";
					textBox.ForeColor = Color.Black;
				}
				MakeHash();
			};
		}
		

		private void MakeHash() {
			var md5hash = md5hasher.ComputeHash(Encoding.UTF8.GetBytes(textBox1.Text + textBox2.Text ));
			md5hash = md5hasher.ComputeHash(md5hash.Concat(Encoding.UTF8.GetBytes(textBox3.Text)).ToArray());
			String result="";
			md5hash.ToList().ForEach(a => result += a.ToString("x2"));
			textBox4.Text = result;
		}

	}
}
