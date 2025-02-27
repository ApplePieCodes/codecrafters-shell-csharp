using System.Net;
using System.Net.Sockets;

public class XSH {
    public static void Main(string[] args) {
        Console.Write("$ ");
        string command = Console.ReadLine();
        Console.WriteLine($"{command}: command not found");
    }
}