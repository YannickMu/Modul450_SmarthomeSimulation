using SmartHomeSimulation;

namespace M320_SmartHome {
    public class Schlafzimmer : Zimmer, ILueftung {
        bool LueftungAn { get; set; }
        public Wetterdaten Wetter { get; set; }

        public Schlafzimmer() : base("Schlafen") {
        }

        public void PruefeLueftung()
        {
            LueftungAn = Temperaturvorgabe > Wetter.Aussentemperatur && !Wetter.Regen && !PersonenImZimmer;
        }
    }
}
