# An example API to test file uploading
### Swagger

- Run the API and navigate to [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html).
- Click 'try it out' and upload your desired files
- Execute. 
    - The requests uses `Content-Type: multipart/form-data` to allow different file types
    - The cURL request is:
        > curl -X POST "https://localhost:5001/File/Upload" -H  "accept: */*" -H  "Content-Type: multipart/form-data" -F "files=@larry.jpg;type=image/jpeg" -F "files=@lorem.txt;type=text/plain" -F "files=@sample.pdf;type=application/pdf"
    - Each file is specified with a -F (form) parameter: "files=@larry.jpg;type=image/jpeg"
![image](https://user-images.githubusercontent.com/54287715/140987583-2c2b48c3-ed83-4f74-aac3-a96d0079f875.png)
- View  uploaded files the `Files` folder in the API 
![image](https://user-images.githubusercontent.com/54287715/140987417-807b0d08-9103-47e5-a92a-6e3559d1814b.png)
