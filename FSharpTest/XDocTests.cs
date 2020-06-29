using System;
using System.Linq;
using System.Xml.Linq;

namespace FSharpTest
{
    internal static class XDocTests
    {
        public static void Run()
        {
            const string str = "<archived xmlns=\"urn: xmpp: mam: tmp\" by=\"test@jabber.ru\" id=\"1593411609820508\" /><stanza-id xmlns=\"urn:xmpp:sid:0\" by=\"test@jabber.ru\" id=\"1593411609820508\" /><origin-id xmlns=\"urn:xmpp:sid:0\" id=\"31e8d546-6dfb-4a2f-8653-a6b013a484f2\" /><request xmlns=\"urn:xmpp:receipts\" /><delay xmlns=\"urn:xmpp:delay\" from=\"jabber.ru\" stamp=\"2020-06-29T06:20:09.837609Z\">Offline Storage</delay><body xmlns=\"jabber:client\">привет</body><thread xmlns=\"jabber:client\">dMxrineAbwfIXzullRYLcwgRCGMkkCLn</thread>";

            var data = XDocument.Parse($"<data>{str}</data>");
            var delay_nodes = data.Descendants(XName.Get("delay", "urn:xmpp:delay"));
            var delay_node = delay_nodes.First();
            var delay = (DateTime)delay_node.Attribute("stamp");

            Console.ReadLine();
        }
    }
}
