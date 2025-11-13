using SmartHomeSimulation;

namespace M320_SmartHome {
    public class Wohnzimmer : Zimmer, ILueftung {
        public bool LueftungAn { get; private set; }
        private readonly ILogger _logger;
        public Wetterdaten Wetter { get; set; }

        public Wohnzimmer(ILogger logger = null) : base("Wohnzimmer")
        {
            _logger = logger ?? new NullLogger();
        }

        public void PruefeLueftung()
        {
            LueftungAn = Temperaturvorgabe > Wetter.Aussentemperatur && !Wetter.Regen && PersonenImZimmer;
            _logger.Log($"[{Name}] Lueftung: {(LueftungAn ? "Ein" : "Aus")}");
        }

    }
}
