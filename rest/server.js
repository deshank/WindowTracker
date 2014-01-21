var restify = require('restify');

function respond(req, res, next) {
  res.send(req.params.id);
}

var server = restify.createServer();
server.get('/log/:id', respond);
//server.head('/hello/:name', respond);


server.listen(8080, function() {
  console.log('%s listening at %s', server.name, server.url);
});
