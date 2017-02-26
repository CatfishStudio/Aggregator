/* Создание файла базы данных */
			CreateDatabase createDataBase;
			try{
				createDataBase = new CreateDatabase(DataConfig.configFile, DataConstants.TYPE_OLEDB);
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "Ошибка:");
				Application.Exit();
			}
			
			/* Создание таблицы пользователей */
			CreateTable createTable;
			createTable = new CreateTable("Users", DataConfig.configFile, DataConstants.TYPE_OLEDB);
			createTable.СolumnAdd("ID", true, "COUNTER");
			createTable.СolumnAdd("Name");
			createTable.СolumnAdd("Pass");
			createTable.СolumnAdd("Permissions");
			try{
				createTable.Execute();
				createTable.InsertValue("0, 'Администратор', '', 'admin'");
				createTable.InsertValue("1, 'Пользователь', '', 'user'");
			}catch(Exception ex){
				createTable.Error();
				MessageBox.Show(ex.ToString(), "Ошибка:");
				Application.Exit();
			}
			
			DataConfig.localDatabase = DataConfig.resource + "\\database.mdb";
			DataConfig.typeConnection = DataConstants.CONNETION_LOCAL;
			
			/* Создание таблицы настроек */
			createTable = new CreateTable("Settings", DataConfig.configFile, DataConstants.TYPE_OLEDB);
			createTable.СolumnAdd("ID", true, "COUNTER");
			createTable.СolumnAdd("name");
			createTable.СolumnAdd("localDatabase");
			createTable.СolumnAdd("typeDatabase");
			createTable.СolumnAdd("typeConnection");
			createTable.СolumnAdd("server");
			createTable.СolumnAdd("serverUser");
			createTable.СolumnAdd("serverDatabase");
			
			try{
				createTable.Execute();
				createTable.InsertValue("0, 'database', '" + DataConfig.localDatabase 
				                        + "', '" + DataConstants.TYPE_OLEDB 
				                        + "', '" + DataConfig.typeConnection 
				                        + "', 'localhost', 'sa', 'database'"); 
			}catch(Exception ex){
				createTable.Error();
				MessageBox.Show(ex.ToString(), "Ошибка:");
				Application.Exit();
			}