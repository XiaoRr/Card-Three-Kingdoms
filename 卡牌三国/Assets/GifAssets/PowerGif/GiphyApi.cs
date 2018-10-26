using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.GifAssets.PowerGif
{
	public class GiphyApi
	{
		private readonly string _userName;
		private readonly string _apiKey;

		public GiphyApi(string userName, string apiKey)
		{
			_userName = userName;
			_apiKey = apiKey;
		}

		public IEnumerator Upload(string name, byte[] binary, string tags, string link, Action<bool, string> callback)
		{
			var form = new WWWForm();

			form.AddField("username", _userName);
			form.AddField("api_key", _apiKey); // TODO: Use production key! To do this, pass their review.
			form.AddField("tags", tags);
			form.AddField("source_post_url", link);
			form.AddBinaryData("file", binary, name, "application/octet-stream");

			yield return Upload(form, callback);
		}

		private IEnumerator Upload(WWWForm form, Action<bool, string> callback)
		{
			var www = new WWW("https://upload.giphy.com/v1/gifs?api_key=" + _apiKey, form);

			Debug.LogFormat("Downloading: {0}...", www.url);

			while (!www.isDone)
			{
				yield return null;
			}

			if (www.error != null)
			{
				if (www.error.Contains("403 Forbidden"))
				{
					Debug.LogWarning("This exception may be caused by wrong user name or api key. Did you request the production key?");
				}

				throw new Exception(www.error);
			}

			if (www.error == null)
			{
				var matches = Regex.Matches(www.text, "\"data\":{\"id\":\"(\\w+)\"");

				if (matches.Count == 1)
				{
					var id = matches[0].Groups[1].Value;

					callback(true, id);
				}
				else
				{
					callback(false, www.text);
				}
			}
			else
			{
				callback(false, www.error);
			}
		}
	}
}