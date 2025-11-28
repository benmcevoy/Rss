Rss
===
My own google reader replacement.  Basic, but fun to write and does the job.


# Client
requires node installed for the react jsx support
i cannot remember how to build this, i presume `npm run build`



# Server

`./publish.sh`

will publish to the server/build/wwwroot2
expects to be served as http//:api.rss.local:5000 in kestral
edit /etc/hosts for the hostname

hit /swagger for api details

server is installed as service via rss.service

```
sudo systemctl stop rss.service
sudo systemctl start rss.service
sudo systemctl status rss.service
```

# Start Stop ngix


sudo systemctl stop nginx
sudo systemctl start nginx

# file locked?

lsof /path/to/file

dotnet has a lock on it often
kill the pid

# 403 permissions denied

ensure the www-data has read access all the way up the path

for me I ended up using nautlius and allow "access files" for other on each folder.

e.g. allow access

- /media
- /media/foo
- etc

annoying.  in prod it would be more like /www/site which is more sensible

using `sudo -u www-data /bin/bash` to start a terminal as `www-data` (the nignx user) was helpful in discovering I had no permission to even look at the mount I was using.


