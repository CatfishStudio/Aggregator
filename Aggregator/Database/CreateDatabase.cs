/*
 * Created by SharpDevelop.
 * User: Somov Studio
 * Date: 24.02.2017
 * Time: 7:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using ADOX;
using Aggregator.Data;

namespace Aggregator.Database
{
	/// <summary>
	/// Description of CreateDatabase.
	/// 
	/// </summary>
	public class CreateDatabase
	{
		private Catalog ADOXCatalog;
		private String path;
		
		public CreateDatabase(String fileName)
		{
			path = fileName;
			create();
		}
		
		void create()
		{
			ADOXCatalog = new Catalog();
			ADOXCatalog.Create(DataConfig.oledbConnectLineBegin + path + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass);
		}
		
		
		
	}
}
