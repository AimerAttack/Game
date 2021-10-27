using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Core;
using UnityEngine;

namespace GameFrame.Logic
{
    public class HttpComponent : GameFrameComponentBase
    {
        private Dictionary<string, RequestBase> _httpRequests = new Dictionary<string, RequestBase>();
        private Coroutine _lateInit;
        
        protected override void OnAwake()
        {
            _lateInit = Entry.Coroutine.StartCoroutine(LateInit());
        }

        IEnumerator LateInit()
        {
            yield return new WaitUntil(()=>Entry.Event != null);
            
            Entry.Utility.AddListener(WebRequestFailureEventArgs.EventId,OnHttpRequestFailure);
            Entry.Utility.AddListener(WebRequestSuccessEventArgs.EventId,OnHttpRequestSuccess);

            _lateInit = null;
        }
        
        private void OnDestroy()
        {
            if (_lateInit != null)
            {
                Entry.Coroutine.StopCoroutine(_lateInit);
                _lateInit = null;
            }

            
            Entry.Utility.RemoveListener(WebRequestFailureEventArgs.EventId,OnHttpRequestFailure);
            Entry.Utility.RemoveListener(WebRequestSuccessEventArgs.EventId,OnHttpRequestSuccess);
        }

        public HttpRequest CreateRequest(string url)
        {
            var request = new HttpRequest(url);
            return request;
        }
        
        public void EnqueueRequestAndSend(HttpRequest req)
        {
            if (_httpRequests.ContainsKey(req.Url))
            {
                Log.Warning($"HttpComponent:EnqueueRequest request with {req.Url} exist");
                return;
            }

            Log.Info($"HttpComponent:EnqueueRequest {req.Url}");
            _httpRequests.Add(req.Url, req);
            Entry.WebRequest.AddWebRequest(req.Url, req.WwwForm);
        }
        
        private void OnHttpRequestSuccess(object sender, GameEventArgs e)
        {
            var ee = e as WebRequestSuccessEventArgs;
            if (ee == null) return;

            if (!_httpRequests.ContainsKey(ee.WebRequestUri))
            {
                Log.Warning($"HttpComponent:OnHttpRequestSuccess request with {ee.WebRequestUri} not exist");
                return;
            }

            var req = (HttpRequest) _httpRequests[ee.WebRequestUri];
            var responseBytes = ee.GetWebResponseBytes();
            try
            {
                if (req.SuccessCallback != null)
                {
                    req.SuccessCallback.Invoke(req, responseBytes);
                }
            }
            catch (Exception exception)
            {
                ExportException(req, exception, "OnHttpRequestSuccess");
            }
            finally
            {
                if (req.WithInputBlock)
                {
                    //TODO:Cancel Input Block
                }

                _httpRequests.Remove(ee.WebRequestUri);
            } 
        }
        
        private void OnHttpRequestFailure(object sender, GameEventArgs e)
        {
            var ee = e as WebRequestFailureEventArgs;
            if (ee == null) return;

            if (!_httpRequests.ContainsKey(ee.WebRequestUri))
            {
                Log.Warning($"HttpComponent:OnHttpRequestFailure request with {ee.WebRequestUri} not exist");
                return;
            }

            var req = (HttpRequest) _httpRequests[ee.WebRequestUri];
            try
            {
                if (req.RetryTimes <= 0)
                {
                    if (req.FailureCallback != null)
                    {
                        req.FailureCallback.Invoke(req, ee.WebResponseBytes);
                    }
                    else
                    {
                        //TODO:通用错误tips
                    }
                }
            }
            catch (Exception exception)
            {
                ExportException(req, exception, "OnHttpRequestFailure");
            }
            finally
            {
                if (req.RetryTimes > 0)
                {
                    Entry.WebRequest.AddWebRequest(req.Url, req.WwwForm);
                }
                else
                {
                    if (req.WithInputBlock)
                    {
                        //TODO:Cancel Input Block
                    }
                    _httpRequests.Remove(ee.WebRequestUri);
                }

                req.RetryTimes--;
            }
        }
        
        private static void ExportException(RequestBase req, Exception exception, string operation)
        {
            var dict = new Dictionary<string, string>
            {
                ["url"] = req.Url,
                ["catch_msg"] = exception.Message,
                ["catch_stack"] = exception.StackTrace,
            };

            Log.Error($"{exception}\n STACKS:\n{exception.StackTrace}");
        }
    }
}