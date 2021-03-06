using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DotnetSpider.DataFlow.Storage
{
	/// <summary>
	/// 文件保存解析结果(所有解析结果)
	/// 保存路径: [当前程序运行目录]/files/[任务标识]/[request.hash].data
	/// </summary>
	public class FileStorage : FileStorageBase
	{
		public static IDataFlow CreateFromOptions(IConfiguration configuration)
		{
			return new FileStorage();
		}

		protected override async Task StoreAsync(DataFlowContext context)
		{
			var file = Path.Combine(GetDataFolder(context.Request.Owner),
				$"{context.Request.Hash}.dat");
			using var writer = OpenWrite(file);
			await writer.WriteLineAsync("RequestUri:\t" + context.Request.RequestUri);
			var items = context
				.GetData();
			await writer.WriteLineAsync("Data:\t" + JsonConvert.SerializeObject(items));
		}
	}
}
