using System.Text;

namespace XSH {
    public abstract class Token {}
    public class Command : Token {
        public string Name { get; }
        public Command(string name) {
            Name = name;
        }

        public override string ToString()
        {
            return $"Command: {Name}";
        }

    }
    public class Argument : Token {
        public string Value { get; }
        public Argument(string value) {
            Value = value;
        }

        public override string ToString()
        {
            return $"Argument: {Value}";
        }

    }
    
    public enum TokenType {
        Command,
        Argument,
        None
    }

    public class InputParser {
        private static TokenType LastToken = TokenType.None;

        public static List<Token> Parse(string input) {
            List<Token> tokens = [];
            StringBuilder buffer = new StringBuilder();
            int i = 0;
            while (i < input.Length) {
                if (input[i] == ' ') {
                    if (buffer.Length > 0) {
                        if (LastToken == TokenType.None) {
                            tokens.Add(new Command(buffer.ToString()));
                            LastToken = TokenType.Command;
                        } 
                        else {
                            tokens.Add(new Argument(buffer.ToString()));
                            LastToken = TokenType.Argument;
                        }
                        buffer.Clear();
                    }
                    i++;
                } 
                else {
                    buffer.Append(input[i]);
                    i++;
                }
            }
            if (buffer.Length > 0) {
                if (LastToken == TokenType.None) {
                    tokens.Add(new Command(buffer.ToString()));
                } 
                else {
                    tokens.Add(new Argument(buffer.ToString()));
                }
            }
            LastToken = TokenType.None;
            return tokens;
        }
    }

    public class CommandExecutor {
        public static int i = 0;
        public static void Execute(List<Token> tokens) {
            switch (((Command)tokens[i]).Name) {
                case "exit":
                    i++;
                    if (i < tokens.Count) {
                        Environment.Exit(int.Parse(((Argument)tokens[i]).Value));
                    }
                    else {
                        Environment.Exit(0);
                    }
                    break;
                case "echo":
                    i++;
                    while (i < tokens.Count && tokens[i] is Argument) {
                        Console.Write(((Argument)tokens[i]).Value + " ");
                        i++;
                    }
                    Console.WriteLine();
                    break;
                case "type":
                    i++;
                    if (i < tokens.Count) {
                        switch (((Argument)tokens[i]).Value) {
                            case "exit":
                            case "echo":
                            case "type":
                                Console.WriteLine($"{((Argument)tokens[i]).Value} is a shell builtin");
                                break;
                            default:
                                Console.WriteLine($"{((Argument)tokens[i]).Value}: not found");
                                break;
                        }
                    }
                    break;
                default:
                    Console.WriteLine($"{((Command)tokens[i]).Name}: command not found");
                    return;
            }
            i = 0;
        }
    }

    public class XSH {
        public static void Main() {
            while (true) {
                Console.Write("$ ");
                List<Token> command = InputParser.Parse(Console.ReadLine());
                CommandExecutor.Execute(command);
            }
        }
    }
}