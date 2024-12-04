using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using LuaInterface;
using MiscUtil.Conversion;
using MiscUtil.IO;
using UnityEngine.Experimental.Rendering;

namespace VivaFramework {
    public class NetworkManager : Manager {
        
        private NetworkStream _nStream;
        private TcpClient _socket;

        private const int _buffSize = 8192;
        private Byte[] _receiveBuff = new byte[_buffSize];

        public LuaFunction pushCall;
        private int _token = 0;
        
        private object lockObj = new object();

        void Awake() {
            _socket = new TcpClient
            {
                SendTimeout = 1000,
                ReceiveTimeout = 2000,
                NoDelay = true
            };

//            print("NetworkManager - Awake");
        }

        public void Connect()
        {
            if (_socket.Connected == true)
            {
                return;
            }
            try
            {
                string gameServeIP = AppConst.GameServerIP;
#if UNITY_EDITOR
                gameServeIP = "127.0.0.1";
#endif
                IPAddress ipA = IPAddress.Parse(gameServeIP);
                IPEndPoint ip = new IPEndPoint(ipA, AppConst.GameServerPort);
//                _socket.BeginConnect(ip, new AsyncCallback(OnConnected), _socket);
                _socket.Connect(ip);
                _nStream = _socket.GetStream();
                _nStream.BeginRead(_receiveBuff, 0, _buffSize, new AsyncCallback(OnRead), _socket);
            }
            catch (Exception ex)
            {
                Debug.LogError("客户端连接异常：" + AppConst.GameServerIP + " - " + ex.Message);
            }
//            Send("core@login", "{'playerName':'木哈哈哈'}");
        }

        private void OnRead(IAsyncResult result)
        {
            int len = _nStream.EndRead(result);
//            print("OnRead " + len);
            MemoryStream stream = new MemoryStream();
            EndianBinaryReader reader = new EndianBinaryReader(EndianBitConverter.Big, stream);

//            lock (lockObj)
//            {
              
                stream.Seek(0, SeekOrigin.End);
                stream.Write(_receiveBuff, 0, len);
                stream.Seek(0, SeekOrigin.Begin);
    
                while (len > 0)
                {
                    int dataLen = reader.ReadInt32();
                    print("OnRead Length:" + dataLen);
                    len = len - dataLen - 4;
                    string command = Encoding.UTF8.GetString(reader.ReadBytes(32));
    //                print("OnRead Command:" + command);
                    int token = reader.ReadInt32();
    //                print("OnRead Token:" + token);
                    string content = Encoding.UTF8.GetString(reader.ReadBytes(dataLen - 4 - 32));
    //                print("OnRead Content:" + content);
    
    
                    if (command.Substring(0, 4) == "push")
                    {
    //                print("OnRead 推送 " + pushCall);
                        if (pushCall != null)
                        {
                            try
                            {
                                pushCall.Call(command, content);
                            }
                            catch (Exception e)
                            {
                                Debug.LogError(e.Message);
                            }
                        }
                    }
                    else
                    {
                        if (_luaCallBacks.ContainsKey(token) == true)
                        {
                            LuaFunction callBack = _luaCallBacks[token];
                            _luaCallBacks.Remove(token);
                            try
                            {
                                callBack.Call(content);
                            }
                            catch (Exception e)
                            {
                                Debug.LogError(e.Message);
                            }
                        }
    //                print("OnRead 请求返回");
                    }
                }
                
                _nStream.BeginRead(_receiveBuff, 0, _buffSize, new AsyncCallback(OnRead), _socket);
                  
//            }
            
        }

        
        private Dictionary<int, LuaFunction> _luaCallBacks = new Dictionary<int, LuaFunction>();
        /// <summary>
        ///  向服务器发送请求
        /// </summary>
        /// <param name="action"></param>
        /// <param name="content"></param>
        public void Send(string action, string content, LuaFunction callBack = null)
        {
            MemoryStream stream = new MemoryStream();
            EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream);
            
            
            ///command
            Byte[] command = new byte[32];
            Encoding.UTF8.GetBytes(action).CopyTo(command, 0);
            writer.Write(command);

            _token++;
            ///token
            writer.Write(_token);
            
            ///content
            writer.Write(Encoding.UTF8.GetBytes(content));
            
            writer.Flush();
            Byte[] bytes = stream.ToArray();
            writer.Dispose();
            
            stream = new MemoryStream();
            writer = new EndianBinaryWriter(EndianBitConverter.Big, stream);
            writer.Write(bytes.Length);
            writer.Write(bytes);
            writer.Flush();
            bytes = stream.ToArray();
            writer.Dispose();
            
            
            if (callBack != null)
            {
                _luaCallBacks[_token] = callBack;
            }
            _nStream.Write(bytes, 0, bytes.Length);
        }


        public void Unload() {
            if (_socket != null)
            {
                _socket.Close();
            }
        }


        private void OnDestroy() {
            Unload();
            Debug.Log("~NetworkManager was destroy");
        }
    }
}