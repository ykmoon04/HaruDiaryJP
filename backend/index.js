const express = require('express');
const { mongoDB } = require('./config/mongodb.js');

const User = require('./models/user.model.js');
const userRoute = require('./routes/user.route.js');

const Diary = require('./models/diary.model.js');
const diaryRoute = require('./routes/diary.route.js');

const Tree = require('./models/tree.model.js');
const treeRoute = require('./routes/tree.route.js');

const app = express();

// middleware
app.use(express.json());
app.use(express.urlencoded({ extended: false }));

//routes
app.use('/api/users', userRoute);
app.use('/api/diaries', diaryRoute);
app.use('/api/trees', treeRoute);

mongoDB();
app.listen(3000, () => {
  console.log('server running');
});

// visit default page
app.get('/', (req, res) => {
  res.send('Hello from Node');
});
