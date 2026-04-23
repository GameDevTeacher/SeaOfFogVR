using System;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class FmodController : MonoBehaviour
{
    public static FmodController current;
    
    
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
        UpdateSection(0);
        _ambienceInstance = RuntimeManager.CreateInstance("Event:/Game/Havtåke");
        _ambienceInstance.start();
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
    
        public int RowSection = 2; //2 is the first row
        public int currentSection;
        private EventInstance _ambienceInstance;
        [SerializeField] private bool _ambienceIsPlaying;
        

        public void UpdateSection(int paramvalue)
        {
            // parameter value = the section of music to be played See Havtåke VR/AudioSpace/FMOD
            _ambienceInstance = RuntimeManager.CreateInstance("Event:/Game/Havtåke");
            
            
            RuntimeManager.StudioSystem.setParameterByName("Section", paramvalue);
            Debug.Log("Section parameter changed");
            
            /*else if (!_ambienceIsPlaying)
            {
                Debug.Log("_ambienceinstance should be started");
                _ambienceInstance.start();
                RuntimeManager.StudioSystem.setParameterByName("Section", paramvalue);
                
                _ambienceIsPlaying = true;
            }*/
            currentSection = paramvalue;
        }
        
        public void UpdateBoatSection()
        {
            if (currentSection < RowSection)
            {
                if(RowSection != currentSection)
                {
                    UpdateSection(RowSection); print("rowSection triggered");
                }
                else if (RowSection == currentSection)
                {
                    print("it is already playing");
                }
                
            }
        }
        public void UpdateFishmanIsle(int paramvalue)
        {
        /*  0 = Intro (loop)
            1 = Find the body (loop)
            2 = End (no loop)   */
            RuntimeManager.StudioSystem.setParameterByName("The Fishermen", paramvalue);
        }

        public void UpdateLighthouseReturn(int paramvalue)
        {
            /*  0 = Village
            1 = Church
            2 = Cave   */
            RuntimeManager.StudioSystem.setParameterByName("The LightHouse (Return)", paramvalue);
            if (paramvalue == 2)
            {
                StartCoroutine(waitAndStartStorm());
            }
        }

        private EventInstance theStormInstance;
        private IEnumerator waitAndStartStorm()
        {
            theStormInstance = RuntimeManager.CreateInstance("Event:/Ambience/Storm");
            yield return new WaitForSeconds(8);
            theStormInstance.start();
            yield return new WaitForSeconds(4);
            theStormInstance.stop(STOP_MODE.ALLOWFADEOUT);
            SceneManager.LoadScene("Game");
            SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
        }
        

        public void stopMusic()
        {
            _ambienceInstance.stop(STOP_MODE.ALLOWFADEOUT);
            
        }

        public void startMusic()
        {
            _ambienceInstance.start();
        }
        
        
        

    #endregion

    #region VOLUME CONTROL

        private Bus masterBus;
        private Bus ambienceBus;
        private Bus musicBus;
        private Bus voicelinesBus;
        private Bus reverbBus;
        [Header("Volume mixer")] 
        [Range(-80f, 10f)] public float masterVolume;
        
        [Range(-80f, 10f)] public float ambienceVolume;
       // [Range(-80f, 10f)] public float EchoVolume;
        [Range(-80f, 10f)] public float musicVolume;
        //[Range(-80f, 10f)] public float sfxVolume;
        [Range(-80f, 10f)] public float voicelinesVolume;
        [Range(-80f, 10f)] public float reverbVolume;

        private void StartVolumeMixer()
        {
            
            masterBus = RuntimeManager.GetBus("bus:/");
            ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
           // echoBus = RuntimeManager.GetBus("bus:/Echo Bus");
            musicBus = RuntimeManager.GetBus("bus:/Music");
           // sfxBus = RuntimeManager.GetBus("bus:/SFX");
            voicelinesBus = RuntimeManager.GetBus("bus:/Voicelines");
            reverbBus = RuntimeManager.GetBus("bus:/Reverb");
        }

        private void UpdateVolumeMixer()
        {
            masterBus.setVolume(DecibelToLinear(masterVolume));
            ambienceBus.setVolume(DecibelToLinear(ambienceVolume));
          //  echoBus.setVolume(DecibelToLinear(EchoVolume));
            musicBus.setVolume(DecibelToLinear(musicVolume));
           // sfxBus.setVolume(DecibelToLinear(sfxVolume));
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
