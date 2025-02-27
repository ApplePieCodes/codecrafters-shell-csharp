public class XSH {
    public static void Main(string[] args) {
        while (true) {
            Console.Write("$ ");
            string command = Console.ReadLine();
            if (command == "exit 0") {
                Environment.Exit(0);
            }
            else {
                Console.WriteLine($"{command}: command not found");
            }
        }
    }
}