/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 01.03.2017
 * Time: 10:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Config;

namespace Aggregator.Client.Settings
{
	/// <summary>
	/// Description of FormSettings.
	/// </summary>
	public partial class FormSettings : Form
	{
		public FormSettings()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void FormSettingsLoad(object sender, EventArgs e)
		{
			ReadingConfig.ReadSettings();
			autoUpdateCheckBox.Checked = DataConfig.autoUpdate;
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) autoUpdateCheckBox.Enabled = true;
			else autoUpdateCheckBox.Enabled = false;
			// today, yesterday, week, month, year
			if(DataConfig.period == "today") periodComboBox.Text = "Сегодня";
			if(DataConfig.period == "yesterday") periodComboBox.Text = "Вчера";
			if(DataConfig.period == "week") periodComboBox.Text = "Неделя";
			if(DataConfig.period == "month") periodComboBox.Text = "Месяц";
			if(DataConfig.period == "year") periodComboBox.Text = "Год";
		}
		void FormSettingsFormClosed(object sender, FormClosedEventArgs e)
		{
			Dispose();
			DataForms.FSettings = null;
		}
		void ButtonCancelClick(object sender, EventArgs e)
		{
			Close();
		}
		void ButtonSaveClick(object sender, EventArgs e)
		{
			if(DataConfig.userPermissions == "user" || DataConfig.userPermissions == "guest") {
				MessageBox.Show("У вас недостаточно прав чтобы выполнить эту операцию.", "Сообщение");
				return;
			}
			
			DataForms.FClient.autoUpdateOff();
			DataConfig.autoUpdate = autoUpdateCheckBox.Checked;
			if(DataConfig.autoUpdate) DataForms.FClient.autoUpdateOn();
						
			if(periodComboBox.Text == "Сегодня") DataConfig.period = "today";
			if(periodComboBox.Text == "Вчера") DataConfig.period = "yesterday";
			if(periodComboBox.Text == "Неделя") DataConfig.period = "week";
			if(periodComboBox.Text == "Месяц") DataConfig.period = "month";
			if(periodComboBox.Text == "Год") DataConfig.period = "year";
			SavingConfig.SaveSettings();
			Close();
		}
	}
}
