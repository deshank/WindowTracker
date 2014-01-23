var restify = require('restify');
var fs = require('fs');

function respond(req, res, next) {
  res.send(req.params.id);
  return next();
}

function processRequest(req, res, next) {
  clientIP = function(req) {
  	return (req.headers['x-forwarded-for'] || '').split(',')[0] || req.connection.remoteAddress || req.socket.remoteAddress || req.connection.socket.remoteAddress;
  }
  send200 = function(res) {
    res.send(200);
  }
  
  var a = "abcdefg";
  fs.appendFile('../logs/'+clientIP(req), req.params.data+'\n', function(err){
    if (err)
      res.send(500+" File Error");
    else
      res.send(200+" Data received"); 
    });
  //console.log(clientIP(req));
  //console.log(req.params);
  //res.send(200+" - Data Received");
  return next();
}



var server = restify.createServer();
server.use(restify.bodyParser());
server.use(function(req, res, next) {
  key=req.params.id;
  if (key == "abc")
    next();
  else
    res.send(404);
});

server.get('/log/:id', processRequest);
//server.head('/hello/:name', respond);
server.put('/log/', processRequest);
server.post('/log/:id', processRequest);

server.listen(8080, function() {
  console.log('%s listening at %s', server.name, server.url);
});
