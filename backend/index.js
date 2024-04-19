const express = require('express');
const { mongoDB } = require('./config/mongodb.js');

const User = require('./models/user.model.js');
const userRoute = require('./routes/user.route.js');

const app = express();

// middleware
app.use(express.json());
app.use(express.urlencoded({ extended: false }));

//routes
app.use('/api/users', userRoute);

mongoDB();
app.listen(3000, () => {
  console.log('server running');
});

// visit default page
app.get('/', (req, res) => {
  res.send('Hello from Node');
});
