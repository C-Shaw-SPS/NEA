using MusicOrganisationTests.Lib.APIFetching;
using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Services;
using System.Reflection;

namespace MusicOrganisationTests.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            PupilService pupilService = new(DatabaseProperties.NAME);
        }
    }
}