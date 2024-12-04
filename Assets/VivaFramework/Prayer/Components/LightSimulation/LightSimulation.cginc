#define ANGLE_2_RADIAN 3.1415926 / 180


// 获取高光
float3 globalFakeSpecular(float3 N,float3 V) 
{
    float3 final = 0;
    float3 L = 0;
    /*
    for (int i = 0; i < MAX_GLOBAL_FAKE_LIGHT_NUM; i++) 
    {
        if(globalFake_LightPosition[i].w == 0) // 方向光
        {
            if(globalFake_LightDirection[i].x != 0 || globalFake_LightDirection[i].y != 0 || globalFake_LightDirection[i].z != 0)
            {
                float3 lightDir = UnityWorldToObjectDir(globalFake_LightDirection[i].xyz);
                L = normalize(-lightDir);
                float3 halfView = V + L;
                float3 specular = pow(max(dot(N,halfView),0),globalFake_LightSpread[i]);
                final *= specular * globalFake_LightColor[i].rgb;
            }
        }
    }*/
    return final;
}

//计算点光源衰减系数
float globalFakePointLightAttenuation(float3 worldPos, float3 lightWorldPos, float radius, float attenFactor) 
{
    float distan = length(lightWorldPos - worldPos);//距离
    float atten = 1 - step(0,distan - radius);
    // f(x) = ax^2 + bx + c
    atten = atten * (1 - pow(distan / radius, attenFactor));
    return atten;
}

//计算投射灯光源衰减系数
float globalFakeProjLightAttenuation(float3 worldPos, float3 lightWorldPos, float3 lightDir, float3 N,
 float angle, float range,
 float attenFactor1, float attenFactor3) 
{
    float3 lightPos = UnityWorldToClipPos(lightWorldPos);
    
    float radius = sin(angle * ANGLE_2_RADIAN) * range;
    float maxRange = (range + radius) / cos(angle * ANGLE_2_RADIAN);//最大受光距离
    float distan = length(lightWorldPos - worldPos);    //距离
    float3 centerPos = lightWorldPos + lightDir * range; //投射中心点
    
    float EdotC = max(0,cos(atan(angle * ANGLE_2_RADIAN))); // 锥形边到中心轴的投影
    float WdotL = max(0,dot(lightDir, normalize(lightWorldPos - worldPos)));// 物体点到光源向量和光照方向的投影
    float2 atten = 1;
    atten.x = (1 - step(0,EdotC - WdotL)) * step(0,dot(N,lightDir));
    
    atten.y = max(0.00001,1 - pow(EdotC / WdotL, attenFactor1));
    atten.y *= range / attenFactor3;
    return atten;
}