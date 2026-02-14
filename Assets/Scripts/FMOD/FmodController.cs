using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class FmodController : MonoBehaviour
{
    public static FmodController current;
    
    [SerializeField] private EventReference TBD_Test;
    
    
    private void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartVolumeMixer();
        
    }

    private void Update()
    {
        UpdateVolumeMixer();
    }

    #region BOAT SFX AND AMBIENCE

        private bool _oarInWater;
        
        public void OarSplash(GameObject target)
        {
            if (!_oarInWater)
            {
                _oarInWater = true;
                RuntimeManager.PlayOneShotAttached("Event:/Ambience/Oar Enters",target);
                Debug.Log("OarSplash enter");
                
            }
            else if (_oarInWater)
            {
                _oarInWater = false;
                
                RuntimeManager.PlayOneShotAttached("Event:/Ambience/Oar Exits", target);
                Debug.Log("OarSplash enter");
            }
        }
        
    #endregion

    #region  SECTION CHANGES

        //private float _CurrentSection;
        private EventInstance _ambienceInstance;
        private bool _ambienceIsPlaying;
        

        public void UpdateSection(int paramvalue)
        {
            // parameter value = the section of music to be played See Havtåke VR/AudioSpace/FMOD
            _ambienceInstance = RuntimeManager.CreateInstance("Event:/Game/Havtåke");
            if (_ambienceIsPlaying)
            {
                RuntimeManager.StudioSystem.setParameterByName("Section", paramvalue);
                Debug.Log("Section parameter changed");
            }
            else if (!_ambienceIsPlaying)
            {
                Debug.Log("_ambienceinstance should be started");
                _ambienceInstance.start();
                RuntimeManager.StudioSystem.setParameterByName("Section", paramvalue);
            }
            
        }

        public void UpdateFishmanIsle(int paramvalue)
        {
        /*  0 = Intro (loop)
            1 = Find the body (loop)
            2 = End (no loop)   */
            RuntimeManager.StudioSystem.setParameterByName("The Fishermen", paramvalue);
        }

        public void UpdateTheLightHouseReturn(int paramvalue)
        {
            /*  0 = Village
            1 = Church
            2 = Cave   */
            RuntimeManager.StudioSystem.setParameterByName("The LightHouse (Return)", paramvalue);
        }
        

        public void stopAmbience()
        {
            _ambienceInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
        
        
        

    #endregion

    #region VOLUME CONTROL

        private Bus masterBus;
        private Bus ambienceBus;
        private Bus echoBus;
        private Bus musicBus;
        private Bus sfxBus;
        private Bus voicelinesBus;
        private Bus reverbBus;
        [Header("Volume mixer")] 
        [Range(-80f, 10f)] public float masterVolume;
        
        [Range(-80f, 10f)] public float ambienceVolume;
        [Range(-80f, 10f)] public float EchoVolume;
        [Range(-80f, 10f)] public float musicVolume;
        [Range(-80f, 10f)] public float sfxVolume;
        [Range(-80f, 10f)] public float voicelinesVolume;
        [Range(-80f, 10f)] public float reverbVolume;

        private void StartVolumeMixer()
        {
            
            masterBus = RuntimeManager.GetBus("bus:/");
            ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
            echoBus = RuntimeManager.GetBus("bus:/Echo Bus");
            musicBus = RuntimeManager.GetBus("bus:/Music");
            sfxBus = RuntimeManager.GetBus("bus:/SFX");
            voicelinesBus = RuntimeManager.GetBus("bus:/Voicelines");
            reverbBus = RuntimeManager.GetBus("bus:/Reverb");
        }

        private void UpdateVolumeMixer()
        {
            masterBus.setVolume(DecibelToLinear(masterVolume));
            ambienceBus.setVolume(DecibelToLinear(ambienceVolume));
            echoBus.setVolume(DecibelToLinear(EchoVolume));
            musicBus.setVolume(DecibelToLinear(musicVolume));
            sfxBus.setVolume(DecibelToLinear(sfxVolume));
            voicelinesBus.setVolume(DecibelToLinear(voicelinesVolume));
            reverbBus.setVolume(DecibelToLinear(reverbVolume));
            
        }

        
        
        private float DecibelToLinear(float dB)
        {
            //Converts our volumedata into decibels
            float linear = Mathf.Pow(10.0f, dB / 20f);
            return linear;
        }

    #endregion

    #region ECHO CONTROL
    
            private EventInstance _echoInstance;
        
        
            public void UpdateEchoTrigger(string filepath)
            {
                RuntimeManager.PlayOneShot(filepath);
            }

            public void Update3DEchoTrigger(string filepath, GameObject gameobject)
            {
                RuntimeManager.PlayOneShotAttached(filepath, gameobject);
            }

            

    #endregion
}
