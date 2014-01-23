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
  
  fs.appendFile('../logs/'+clientIP(req), req.params.data+'\n', function(err){
    if (err)
      res.send(500+" File Error");
    else
      res.send(200+" Data received"); 
    });
  
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

server.post('/log/:id', processRequest);

server.listen(8080, function() {
  console.log('%s listening at %s', server.name, server.url);
});
