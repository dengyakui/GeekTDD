using FluentAssertions;
using GeekTDD.Demo01;
using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Helpers;
using Xunit;


namespace GeekTDD.Tests
{
    public class ArgsTest
    {

        // -l -p 8080 -d /usr/logs
        // [-l], [-p, 8080], [-d, /usr/logs]
        // {-l:[], -p:[8080], -d:[/usr/logs]}
        // Single Option:
        [Fact]
        public void should_get_boolean_option_to_true_if_flag_present()
        {
            var option = Args.Parse<BooleanOption>("-l");
            option.Logging.Should().BeTrue();
        }


        [Fact]
        public void should_get_boolean_option_to_false_if_flag_not_present()
        {
            var option = Args.Parse<BooleanOption>("");
            option.Logging.Should().BeFalse();
        }

        public record MultiOptions([CmdArg("l")] bool Logging, [CmdArg("p")] int Port, [CmdArg("d")] string Directory);

        public record BooleanOption([CmdArg("l")] bool Logging);


        public record ListOptions([CmdArg("g")] string[] Group, [CmdArg("d")] int[] Decimals);



        [Fact]
        public void should_parse_int_as_options_value()
        {
            var intOption = Args.Parse<IntOption>("-p", "8080");
            intOption.Port.Should().Be(8080);
        }

        public record IntOption([CmdArg("p")] int Port);


        [Fact]
        public void should_parse_string_as_options_vale()
        {
            var stringOption = Args.Parse<StringOption>("-d", "/usr/logs");
            stringOption.Directory.Should().Be("/usr/logs");
        }

        public record StringOption([CmdArg("d")]string Directory);




        // TODO: multi options: -l -p 8080 -d /usr/logs
        [Fact]
        public void should_example1()
        {
            var (logging, port, directory) = Args.Parse<MultiOptions>("-l", "-p", "8080", "-d", "/usr/logs");

            port.Should().Be(8080);
            logging.Should().BeTrue();
            directory.Should().Be("/usr/logs");

        }


        // sad path:
        // TODO: - bool -l t/ -l t f
        // TODO: - int -p/ -p 8080 8081
        // TODO: - string -d/ -d /usr/logs /usr/vars
        // default value
        // TODO: - bool: false
        // TODO: - int: 0
        // TODO: - string: ""




        // -g this is a list -d 1 2 -3 5

        [Fact(Skip = "skip now")]
        public void should_example2()
        {
            ListOptions args = Args.Parse<ListOptions>("-g", "this", "is", "a", "list", "-d", "1", "2", "-3", "5");
            
            string[] group = args.Group;
            int[] decimals = args.Decimals;

            group.Should().BeEquivalentTo(new string[] { "this", "is", "a", "list" });
            decimals.Should().BeEquivalentTo(new int[] { 1, 2, -3, 5 });



        }


        


        

    }
}