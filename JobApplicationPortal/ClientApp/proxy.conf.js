const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:8711';

const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast", 
      "/job",
      "/candidate",
      "/quiz",
      "/quiz/submitQuiz",
      "/upload",
      "/admin",
      "/admin/test",
      "/admin/test-list",
      "/admin/test-update",
      "/admin/question",
      "/admin/test-list",
      "/admin/question-update",

   ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
