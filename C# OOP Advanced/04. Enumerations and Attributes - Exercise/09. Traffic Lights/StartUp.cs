using System;
using System.Collections.Generic;

class StartUp
{
    static void Main()
    {
        List<State> allLights = new List<State>();
        var stateLights = Console.ReadLine().Split(' ');
        var loops = int.Parse(Console.ReadLine());
        foreach (var signal in stateLights)
        {
            Lights Light = (Lights)Enum.Parse(typeof(Lights), signal);
            allLights.Add(new State(Light));
        }
        for (int i = 0; i < loops; i++)
        {
            foreach (var light in allLights)
            {
                light.ChangeState();
            }
            Console.WriteLine(string.Join(" ", allLights));
        }
    }
}