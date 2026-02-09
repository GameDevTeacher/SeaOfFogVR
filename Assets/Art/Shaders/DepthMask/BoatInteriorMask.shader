Shader "Custom/BoatInteriorMask" {
    SubShader {
        Tags { 
            "Queue" = "Geometry+10"
            "RenderType" = "Opaque"
        }
        
        Stencil {
            Ref 1
            Comp Always
            Pass Replace
        }
        
        ColorMask 0
        ZWrite On
        
        Pass {}
    }
}