namespace SiriusTap.PriceFeedManager.Common.Services.ContainerServices
{
	public interface ICrud
	{
		void Create(object item);

		T Read<T>(object item);

		void Update(object item);

		void Delete(object item);
	}
}