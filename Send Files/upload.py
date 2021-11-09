import requests as r
import base64
from os import walk

url = "https://localhost:5001/File/Upload"
files = {}
for f in next(walk("upload"), (None, None, []))[2]:
    with open(f'upload/{f}', 'rb') as current:
        files[f] = encoded_string = base64.b64encode(current.read())
print(len(files))
proxies = {'http': 'https://localhost:5001'}
response = r.post(url, files=files, verify=False, proxies=proxies)
print(response.status_code)