const mongoose = require('mongoose');
exports.mongoDB = () => {
  mongoose
    .connect(
      'mongodb+srv://admin:5apRtK0R7IaCzned@backenddb.gjns8te.mongodb.net/NODE-API'
    )
    .then(() => console.log('mongodb connected'))
    .catch(() => console.log('mongodb connection failed'));
};
