Rss
===
My own google reader replacement.  Basic, but fun to write and does the job.


# Client
requires node installed for the react jsx support
i cannot remember how to build this, i presume `npm build`



# Server

`./publish.sh`

will publish to the server/build/wwwroot2
expects to be served as http//:api.rss.local:5000 in kestral
edit /etc/hosts for the hostname

hit /swagger for api details

# Start Stop ngix


sudo systemctl start nginx

# file locked?

lsof /path/to/file

dotnet has a lock on it often
kill the pid