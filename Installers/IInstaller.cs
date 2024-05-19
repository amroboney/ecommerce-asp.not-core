using System;
namespace EcommerceAPI.Installers
{
	public interface IInstaller
	{
		void InstallServices(IServiceCollection service, IConfiguration configuration, IWebHostEnvironment env);
	}
}

