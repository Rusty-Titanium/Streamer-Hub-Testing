console.log('This is the Start of a Test Document for Twitch Bot integration yup beans.');

const tmi = require('tmi.js');
// Define configuration options

const opts = {
    identity: {
        username: 'rabbitslayer',
        password: 'x5y2t850h4fe0hse4m4lyatbyseqp1'
    },
    channels: [
        'tramycakes'
    ]
};

// Create a client with our options
const client = new tmi.client(opts);

// Register our event handlers (defined below)
client.on('message', onMessageHandler);
client.on('connected', onConnectedHandler);

// Connect to Twitch:
client.connect();

// This little section is going to have global variables.
var nameToFocus = "";

// Called every time a message comes in.
function onMessageHandler(target, context, msg, self) {
    if (self) {
        return;
    }

    // Ignore messages from the bot
    console.log("UserName: " + context['display-name']);

    // Remove whitespace from chat message
    const commandName = msg.trim();

    console.log("Message Sent: " + target + "  " + context['display-name'] + "  " + msg + "  " + self);

    // If the command is known, let's execute it
    if (commandName === '!dice') {
        const num = rollDice();
        client.say(target, `You rolled a ${num}`);
        console.log(`* Executed ${commandName} command`);
    }
    else if (commandName === '!setUser') {
        nameToFocus = context['display-name'];
    }
    else {
        console.log(`* Unknown command ${commandName}`);
    }

    //client.say(target, `Try this` + "<@" + context['user-id'] + ">");
    if (nameToFocus.length > 0) {
        if (context['display-name'] === nameToFocus) {
            client.say(target, `So beautiful @` + nameToFocus);
        }
        else {
            client.say(target, `ew, you aren't ` + nameToFocus);
        }
    }
}

// Function called when the "dice" command is issued
function rollDice() {
    const sides = 6;
    return Math.floor(Math.random() * sides) + 1;
}

// Called every time the bot connects to Twitch chat
function onConnectedHandler(addr, port) {
    console.log(`* Connected to ${addr}:${port}`);
}
//# sourceMappingURL=bot.js.map