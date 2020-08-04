using ConsoleEShopMultilayered.BLL;

namespace ConsoleEShopMultilayered.PL
{
    static class Program
    {

        static void Main()
        {
            Controller controller = new Controller();
            ConsoleUI view = new ConsoleUI(null, controller);
            view.Start();
        }
    }
}
