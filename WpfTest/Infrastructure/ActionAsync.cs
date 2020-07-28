using System.Threading.Tasks;

namespace WpfTest.Infrastructure
{
    internal delegate Task ActionAsync();

    internal delegate Task ActionAsync<in T>(T arg);
}
