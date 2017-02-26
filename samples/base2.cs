
			OleDb oleDb;
			oleDb = new OleDb(DataConfig.configFile);
			try{
				oleDb.DataTableClear();
				oleDb.DataTableColumnAdd("name", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("localDatabase", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("typeDatabase", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("typeConnection", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("server", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("serverUser", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("serverDatabase", Type.GetType("System.String"));
				oleDb.SetSelectCommand("SELECT * FROM Settings");
				oleDb.Fill("Settings");
				
				DataConfig.localDatabase = oleDb.GetValue("Settings", 0, "localDatabase").ToString();
				DataConfig.typeDatabase = oleDb.GetValue("Settings", 0, "typeDatabase").ToString();
				DataConfig.typeConnection = oleDb.GetValue("Settings", 0, "typeConnection").ToString();
				DataConfig.server = oleDb.GetValue("Settings", 0, "server").ToString();
				DataConfig.serverUser = oleDb.GetValue("Settings", 0, "serverUser").ToString();
				DataConfig.serverDatabase = oleDb.GetValue("Settings", 0, "serverDatabase").ToString();
				Utilits.Console.Log("Настройки соединения с базой данных успешно загружены.");
			}catch(Exception ex){
				oleDb.Error();
				MessageBox.Show(ex.ToString());
				Application.Exit();
			}
