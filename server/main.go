package main

import (
	"context"
	"fmt"
	pb "github.com/fearlesshyena/grpccolorgen/protocolor"
	"google.golang.org/grpc"
	"log"
	"math/rand"
	"net"
)

const (
	port       = ":50051"
	colorBytes = 3
)

type server struct {}

//Return a random color in the form of a color hex string
//e.g. "#00FF00" would be RRGGBB so Green
func (s *server) GetRandomColor(ctx context.Context, curr *pb.CurrentColor) (*pb.NewColor, error) {
	hex := "#" + randomHex()
	log.Printf("Client's current color: [#%v] sending [%v]", curr.Color, hex)
	return &pb.NewColor{Color: hex}, nil
}

//Create a random hex string of N digits
func randomHex() string {
	bytes := make([]byte, colorBytes)
	if _, err := rand.Read(bytes); err != nil {
		log.Panicln("Error generating random hex value", err)
	}
	return fmt.Sprintf("%X", bytes)
}

//Create a gRPC server and listen for incoming requests
func main() {
	lis, err := net.Listen("tcp", port)
	if err != nil {
		log.Fatalf("Failed to listen on port [%s]: %v", port, err)
	}
	s := grpc.NewServer()
	pb.RegisterColorGeneratorServer(s, &server{})
	if err := s.Serve(lis); err != nil {
		log.Fatalf("Failed to start the server: %v", err)
	}
}
