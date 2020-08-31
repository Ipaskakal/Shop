using ConsoleEShopMultilayered.BLL;
using Ninject;

namespace ConsoleEShopMultilayered.PL
{
    static class Program
    {

        static void Main()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IController>().To<Controller>();
            IController controller = kernel.Get<IController>();
            ConsoleUI view = new ConsoleUI(null, controller);
            view.Start();
        }
    }
}
